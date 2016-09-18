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
using System.Collections;
using ChatMe.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;

namespace ChatMe.Web.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageService messageService;
        private IChatHubService hubService;

        public ChatHub(IMessageService messageService, IChatHubService chatHubService) : base() {
            this.messageService = messageService;
            hubService = chatHubService;
        }

        public override async Task OnConnected() {
            Debug.Print($"User connected {Context.User.Identity.Name}");
            // Retrieve user.
            var userId = Context.User.Identity.GetUserId();

            // Check if pending offline
            var isNewUser = await hubService.Connect(userId, Context.ConnectionId);
            if (isNewUser) {
                foreach (var dialogId in hubService.GetUserDialogIds(userId)) {
                    await Groups.Add(Context.ConnectionId, dialogId.ToString());
                }

                Clients.Clients(hubService.GetOnlineIds()).notifyOnline(userId, true);
            }

            await base.OnConnected();
        }

        public async Task Send(int dialogId, NewMessageViewModel message) {
            var newMessageData = new NewMessageDTO {
                UserId = Context.User.Identity.GetUserId(),
                DialogId = dialogId,
                Body = message.Body
            };

            var createdMessageData = await messageService.Create(newMessageData);
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()
                .ForMember("AuthorAvatarUrl", opt => opt.MapFrom(m => $"/avatars/{m.AuthorId}"))
            );

            var createdMessage = Mapper.Map<MessageViewModel>(createdMessageData);

            Clients.Group(dialogId.ToString()).addMessage(createdMessage);
        }

        public override Task OnDisconnected(bool stopCalled) {
            var userId = Context.User.Identity.GetUserId();

            hubService.Disconnect(userId, () => {
                Clients.Clients(hubService.GetOnlineIds()).notifyOnline(userId, false);
            });

            Debug.Print($"User disconnected {Context.User.Identity.Name}");
            return base.OnDisconnected(stopCalled);
        }
    }   
}