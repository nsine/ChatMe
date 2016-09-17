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
        private IUnitOfWork db;
        private IMessageService messageService;

        private ICollection<User> onlineUsers;
        private ICollection<OfflineState> pendingOffline;

        public ChatHub(IUnitOfWork unitOfWork, IMessageService messageService) : base() {
            this.db = unitOfWork;
            this.messageService = messageService;

            onlineUsers = new List<User>();
            pendingOffline = new List<OfflineState>();
        }

        public override Task OnConnected() {
            Debug.Print($"User connected {Context.User.Identity.Name}");
            // Retrieve user.
            var user = db.Users
                .Users.Where(u => u.UserName == Context.User.Identity.Name)
                .FirstOrDefault();
            
            if (user == null) {
                throw new Exception($"User {Context.User.Identity.Name} not found");
            }

            // Check if pending offline
            var offlineState = pendingOffline.Where(s => s.User.Id == user.Id).FirstOrDefault();
            if (offlineState == null) {
                onlineUsers.Add(user);

                foreach (var dialog in user.Dialogs) {
                    Groups.Add(Context.ConnectionId, dialog.Id.ToString());
                }

                Clients.All.notifyOnline(user.Id, true);
            } else {
                offlineState.CancelTokenSource.Cancel();
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
                .ForMember("AuthorAvatarUrl", opt => opt.MapFrom(m => $"/avatars/{m.AuthorId}"))
            );

            var createdMessage = Mapper.Map<MessageViewModel>(createdMessageData);

            Clients.Group(dialogId.ToString()).addMessage(createdMessage);
        }

        public override Task OnDisconnected(bool stopCalled) {
            var user = db.Users
                .Users.Where(u => u.UserName == Context.User.Identity.Name)
                .FirstOrDefault();

            Debug.Print($"User disconnected {Context.User.Identity.Name}");
            return base.OnDisconnected(stopCalled);
        }

        private async Task MakeOffline(User user) {
            var state = new OfflineState {
                CancelTokenSource = new CancellationTokenSource(),
                User = user
            };

            pendingOffline.Add(state);

            await Task.Factory.StartNew(async () => {
                for (int i = 0; i < 10; i++) {
                    Thread.Sleep(1000);
                    if (state.CancelTokenSource.Token.IsCancellationRequested) {
                        pendingOffline.Remove(state);
                        return;
                    }
                }

                user.IsOnline = false;
                onlineUsers.Remove(user);
                pendingOffline.Remove(state);
                Clients.All.notifyOnline(state.User, false);
                await db.Users.UpdateAsync(state.User);
            }, state.CancelTokenSource.Token);
        }
    }

    class OfflineState
    {
        public User User { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
    }
}