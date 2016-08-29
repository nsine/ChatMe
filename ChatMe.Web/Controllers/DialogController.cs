using ChatMe.BussinessLogic;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/dialogs")]
    public class DialogController : Controller
    {
        private IUnitOfWork unitOfWork;

        private AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        public DialogController(IUnitOfWork uow) {
            unitOfWork = uow;
        }


        [HttpGet]
        [Route("")]
        public ActionResult GetAll(int startIndex = 0, int count = 0) {
            var me = UserManager.FindById(User.Identity.GetUserId());
            var dialogs = me.Dialogs
                .OrderByDescending(d => (d.LastMessageTime.HasValue ? d.LastMessageTime : d.CreateTime))
                .Skip(startIndex)
                .Select(d => new DialogViewModel(d, me) {
                    AvatarUrl = Url.Action("GetAvatar", "User", new { id = "todo" })
                });

            if (count != 0) {
                dialogs = dialogs.Take(count);
            }

            return Json(dialogs.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("")]
        public void Post(NewDialogViewModel dialogModel) {
            var users = dialogModel.UserIds
                .Select(id => unitOfWork.Users.Get(id))
                .ToList();

            var newDialog = new Dialog {
                Users = users,
                CreateTime = DateTime.Now
            };

            unitOfWork.Dialogs.Create(newDialog);
            unitOfWork.Save();
        }

        [HttpDelete]
        [Route("")]
        public void Delete(int dialogId) {
            unitOfWork.Dialogs.Delete(dialogId);
            unitOfWork.Save();
        }
    }
}