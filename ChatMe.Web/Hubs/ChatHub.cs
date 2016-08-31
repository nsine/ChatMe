using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ChatMe.Web.Models;
using System.Threading.Tasks;
using ChatMe.DataAccess.Interfaces;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.BussinessLogic.DTO;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Web.Mvc;
using System.IO;
using System.Web.Routing;
using ChatMe.BussinessLogic.Services;
using System.Web.Helpers;

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
            // Retrieve user.
            var user = unitOfWork.Users
                .Find(u => u.UserName == Context.User.Identity.Name)
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

        //public void AddToRoom(string roomName) {
        //    using (var db = new UserContext()) {
        //        // Retrieve room.
        //        var room = db.Rooms.Find(roomName);

        //        if (room != null) {
        //            var user = new User() { UserName = Context.User.Identity.Name };
        //            db.Users.Attach(user);

        //            room.Users.Add(user);
        //            db.SaveChanges();
        //            Groups.Add(Context.ConnectionId, roomName);
        //        }
        //    }
        //}

        //public void RemoveFromRoom(string roomName) {
        //    using (var db = new UserContext()) {
        //        // Retrieve room.
        //        var room = db.Rooms.Find(roomName);
        //        if (room != null) {
        //            var user = new User() { UserName = Context.User.Identity.Name };
        //            db.Users.Attach(user);

        //            room.Users.Remove(user);
        //            db.SaveChanges();

        //            Groups.Remove(Context.ConnectionId, roomName);
        //        }
        //    }
        //}
    }
}