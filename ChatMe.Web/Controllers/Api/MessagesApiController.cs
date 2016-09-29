using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using ChatMe.Web.Models;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using System.Threading.Tasks;
using AutoMapper;
using System.Web.Http;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
        private IMessageService messageService;

        public MessagesController(IMessageService messageService) {
            this.messageService = messageService;

            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()
                .ForMember("AuthorAvatarUrl", opt => opt.MapFrom(m =>
                   Url.Route("Avatar", new { userId = m.AuthorId }))
                )
            );
        }

        [HttpGet]
        [Route("{dialogId}")]
        public IEnumerable<MessageViewModel> GetList(int dialogId, int startIndex = 0, int count = 0) {
            var userId = User.Identity.GetUserId();
            var messagesData = messageService.GetChunk(userId, dialogId, startIndex, count);

            var messages = Mapper.Map<IEnumerable<MessageViewModel>>(messagesData);

            return messages;
        }

        [HttpPost]
        [Route("{dialogId}")]
        public async Task New(int dialogId, NewMessageViewModel newMessageModel) {
            var newMessageData = new NewMessageDTO {
                UserId = User.Identity.GetUserId(),
                DialogId = dialogId,
                Body = newMessageModel.Body
            };

            await messageService.Create(newMessageData);
        }
    }
}