using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/dialogs")]
    public class DialogsController : Controller
    {
        private IDialogService dialogService;

        public DialogsController(IDialogService dialogService) {
            this.dialogService = dialogService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAll(int startIndex = 0, int count = 0) {
            var userId = User.Identity.GetUserId();
            var dialogsData = dialogService.GetChunk(userId, startIndex, count);
            var dialogs = dialogsData
                .Select(d => new DialogViewModel(d) {
                    AvatarUrl = Url.Action("GetAvatar", "Users", new { id = "todo" })
                });

            return Json(dialogs.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("")]
        public async Task<int> Create(NewDialogViewModel dialogModel) {
            Mapper.Initialize(cfg => cfg.CreateMap<NewDialogViewModel, NewDialogDTO>());
            var newDialogData = Mapper.Map<NewDialogDTO>(dialogModel);

            return await dialogService.Create(newDialogData);
        }

        [HttpDelete]
        [Route("")]
        public void Delete(int dialogId) {
            dialogService.Delete(dialogId);
        }

        [HttpGet]
        public async Task<ActionResult> NewDialog(string userId) {
            var myId = User.Identity.GetUserId();
            var memberIds = new List<string> { myId, userId };

            var dialogId = dialogService.GetIdByMembers(memberIds);

            if (dialogId == -1) {
                var newDialogViewModel = new NewDialogViewModel {
                    UserIds = memberIds
                };

                dialogId = await Create(newDialogViewModel);
            }

            return RedirectToAction("Messages", "User", new { dialogId = dialogId });
        }
    }
}