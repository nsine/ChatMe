using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ChatMe.DataAccess.Interfaces;
using ChatMe.Web.Models;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : Controller
    {
        private User me;
        private IUnitOfWork unitOfWork;

        private AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        public MessageController(IUnitOfWork uow) {
            unitOfWork = uow;
            me = UserManager.FindById(User.Identity.GetUserId());
        }


        [HttpGet]
        public ActionResult GetAll(int userId, int startIndex = 0, int count = 0) {
            var user = unitOfWork.Users.Get(userId);
            var posts = user.Posts
                .OrderByDescending(p => p.Time)
                .Skip(startIndex)
                .Select(p => new PostViewModel {
                    Id = p.Id,
                    Body = p.Body,
                    Time = p.Time,
                    Likes = 0,
                    AvatarUrl = Url.Action("GetAvatar", "User", new { id = p.User.Id }),
                    Author = p.User.DisplayName,
                    AuthorLink = Url.RouteUrl("UserProfile", new { id = p.User.Id })
                });

            if (count != 0) {
                posts = posts.Take(count);
            }

            var a = posts.ToList();

            return Json(posts.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{userId}/{postId}")]
        public Post Get(string userId, int postId) {
            return unitOfWork.Users
                .Get(userId).Posts
                .Where(p => p.Id == postId)
                .FirstOrDefault();
        }

        [HttpPost]
        [Route("{userId}")]
        public void Post(string userId, NewPostViewModel postModel) {
            var user = unitOfWork.Users.Get(userId);
            var newPost = new Post {
                Body = postModel.Body,
                User = user,
                Time = DateTime.Now
            };

            unitOfWork.Posts.Create(newPost);
            unitOfWork.Save();
        }

        [HttpPut]
        [Route("{postId}")]
        public void Put(int postId, NewPostViewModel postModel) {
            var post = unitOfWork.Posts.Get(postId);
            post.Body = postModel.Body;
            unitOfWork.Posts.Update(post);
            unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{postId}")]
        public void Delete(int postId) {
            unitOfWork.Posts.Delete(postId);
            unitOfWork.Save();
        }
    }
}