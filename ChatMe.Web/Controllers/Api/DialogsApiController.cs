using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/dialogs")]
    public class DialogsApiController : ApiController
    {
        private IDialogService dialogService;

        public DialogsApiController(IDialogService dialogService) {
            this.dialogService = dialogService;

            Mapper.Initialize(cfg => cfg.CreateMap<NewDialogViewModel, NewDialogDTO>());
        }

        [HttpGet]
        [Route("{dialogId}")]
        public DialogViewModel Get(int dialogId) {
            var myId = User.Identity.GetUserId();
            var dialogData = dialogService.GetById(dialogId);

            // dialogData.Users contains current user but we don't need it
            // FIX it but for now just delete
            dialogData.Users = dialogData.Users.Except(dialogData.Users.Where(u => u.Id == myId));

            var dialog = new DialogViewModel(dialogData) {
                AvatarUrl = Url.Route("Avatar", new {
                    userId = dialogData.Users
                            .Where(u => u.Id != myId)
                            .FirstOrDefault().Id
                })
            };

            return dialog;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<DialogViewModel> GetAll(int startIndex = 0, int count = 0) {
            var myId = User.Identity.GetUserId();
            var dialogsData = dialogService.GetChunk(myId, startIndex, count);
            var dialogs = dialogsData
                .Select(d => new DialogViewModel(d) {
                    AvatarUrl = Url.Route("Avatar", new {
                        userId = d.Users
                            .Where(u => u.Id != myId)
                            .FirstOrDefault().Id
                    })
                });

            return dialogs;
        }

        [HttpPost]
        [Route("")]
        public async Task<int> Create(NewDialogViewModel dialogModel) {
            var newDialogData = Mapper.Map<NewDialogDTO>(dialogModel);

            return await dialogService.Create(newDialogData);
        }

        [HttpDelete]
        [Route("")]
        public void Delete(int dialogId) {
            dialogService.Delete(dialogId);
        }
    }
}