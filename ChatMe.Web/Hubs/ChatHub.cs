using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ChatMe.Web.Models;
using System.Threading.Tasks;
using ChatMe.DataAccess.Interfaces;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.BussinessLogic.DTO;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Diagnostics;

namespace ChatMe.Web.Hubs
{
    public class ChatHub : Hub
    {
        private IUnitOfWork unitOfWork;
        private IMessageService messageService;

        public ChatHub(IUnitOfWork unitOfWork, IMessageService messageService) : base() {
            this.unitOfWork = unitOfWork;
            this.messageService = messageService;
        }

        public override Task OnConnected() {
            Debug.Print($"User connected {Context.User.Identity.Name}");
            // Retrieve user.
            var user = unitOfWork.Users
                .Users.Where(u => u.UserName == Context.User.Identity.Name)
                .FirstOrDefault();
            
            if (user == null) {
                throw new Exception("dib");
            }

            foreach (var dialog in user.Dialogs) {
                Groups.Add(Context.ConnectionId, dialog.Id.ToString());
            }

            return base.OnConnected();
        }

        public async Task Send(int dialogId, NewMessageViewModel message) {
            var newMessageData = new NewMessageDTO {
                UserId = Context.User.Identity.GetUserId(),
                DialogId = dialogId,
                Body = message.Body
            };

            var createdMessageData = await messageService.Create(newMessageData);
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()
                .ForMember("AuthorAvatarUrl", opt => opt.MapFrom(m => $"/avatar/{m.AuthorId}"))
            );

            var createdMessage = Mapper.Map<MessageViewModel>(createdMessageData);

            Clients.Group(dialogId.ToString()).addMessage(createdMessage);
        }

        public override Task OnDisconnected(bool stopCalled) {
            Debug.Print($"User disconnected {Context.User.Identity.Name}");
            return base.OnDisconnected(stopCalled);
        }
    }
}