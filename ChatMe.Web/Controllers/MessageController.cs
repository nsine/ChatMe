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
using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Repositories;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : Controller
    {
        private AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        [HttpGet]
        [Route("{dialogId}")]
        public ActionResult GetList(int dialogId, int startIndex = 0, int count = 0) {
            IUnitOfWork unitOfWork = new EFUnitOfWork();
            var me = UserManager.FindById(User.Identity.GetUserId());
            var dialog = unitOfWork.Dialogs.Get(dialogId);
            var messages = dialog.Messages
                .OrderByDescending(m => m.Time)
                .Skip(startIndex)
                .Select(m => new MessageViewModel {
                    Id = m.Id,
                    Body = m.Body,
                    Time = m.Time,
                    AuthorAvatarUrl = Url.Action("GetAvatar", "User", new { id = m.User.Id }),
                    Author = m.User.DisplayName,
                    IsMy = m.User.Id == User.Identity.GetUserId()
                });

            if (count != 0) {
                messages = messages.Take(count);
            }
            
            return Json(messages.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("{dialogId}")]
        public void New(int dialogId, NewMessageViewModel messageModel) {
            var db = HttpContext.GetOwinContext().Get<ChatMeContext>();
            var id = User.Identity.GetUserId();

            var me = UserManager.FindById(User.Identity.GetUserId());
            var newMessage = new Message {
                Body = messageModel.Body,
                User = me,
                Time = DateTime.Now,
                Dialog = db.Dialogs.Find(dialogId)
            };

            // Fix this shit

            db.Messages.Add(newMessage);
            db.SaveChanges();
        }
    }
}