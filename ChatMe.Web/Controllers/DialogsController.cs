using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    public class DialogsController : Controller
    {
        private IDialogService dialogService;

        public DialogsController(IDialogService dialogService) {
            this.dialogService = dialogService;

            Mapper.Initialize(cfg => cfg.CreateMap<NewDialogViewModel, NewDialogDTO>());
        }

        [Route("dialogs/open/{userId}")]
        [HttpGet]
        public async Task<ActionResult> OpenOrCreateDialog(string userId) {
            var myId = User.Identity.GetUserId();
            var memberIds = new List<string> { myId, userId };

            var dialogId = dialogService.GetIdByMembers(memberIds);

            if (dialogId == -1) {
                var newDialogViewModel = new NewDialogViewModel {
                    UserIds = memberIds
                };

                var newDialogData = Mapper.Map<NewDialogDTO>(newDialogViewModel);
                dialogId = await dialogService.Create(newDialogData);
            }

            return RedirectToAction("Messages", "Users", new { dialogId = dialogId });
        }
    }
}