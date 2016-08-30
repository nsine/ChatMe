using AutoMapper;
using ChatMe.BussinessLogic;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using ChatMe.Web.Controllers.Abstract;
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
    public class DialogController : IdentityController
    {
        private IDialogService dialogService;

        public DialogController(IDialogService dialogService) {
            this.dialogService = dialogService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAll(int startIndex = 0, int count = 0) {
            var userId = User.Identity.GetUserId();
            var dialogsData = dialogService.GetChunk(UserManager, userId, startIndex, count);
            var dialogs = dialogsData
                .Select(d => new DialogViewModel(d) {
                    AvatarUrl = Url.Action("GetAvatar", "User", new { id = "todo" })
                });

            return Json(dialogs.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("")]
        public void Post(NewDialogViewModel dialogModel) {
            Mapper.Initialize(cfg => cfg.CreateMap<NewDialogViewModel, NewDialogDTO>());
            var newDialogData = Mapper.Map<NewDialogDTO>(dialogModel);

            dialogService.Create(newDialogData);
        }

        [HttpDelete]
        [Route("")]
        public void Delete(int dialogId) {
            dialogService.Delete(dialogId);
        }
    }
}