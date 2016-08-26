﻿using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using ChatMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostController : Controller
    {
        private IUnitOfWork unitOfWork;

        public PostController(IUnitOfWork uow) {
            unitOfWork = uow;
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult GetAll(string userId, int startIndex = 0, int count = 0) {
            var user = unitOfWork.Users.Get(userId);
            var posts = user.Posts.Skip(startIndex);
            if (count != 0) {
                posts = posts.Take(count);
            }

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
