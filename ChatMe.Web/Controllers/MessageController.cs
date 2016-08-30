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
using ChatMe.Web.Controllers.Abstract;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using System.Threading.Tasks;
using AutoMapper;

namespace ChatMe.Web.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : IdentityController
    {
        private IMessageService messageService;

        public MessageController(IMessageService messageService) {
            this.messageService = messageService;
        }

        [HttpGet]
        [Route("{dialogId}")]
        public ActionResult GetList(int dialogId, int startIndex = 0, int count = 0) {
            var userId = User.Identity.GetUserId();
            var messagesData = messageService.GetChunk(userId, dialogId, startIndex, count);

            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()
                .ForMember("AuthorAvatarUrl", opt => opt.MapFrom(m =>
                    Url.Action("GetAvatar", "User", new { id = m.AuthorId }))
                )
            );

            var messages = Mapper.Map<IEnumerable<MessageViewModel>>(messagesData);
            
            return Json(messages.ToList(), JsonRequestBehavior.AllowGet);
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