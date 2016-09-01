using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;

namespace ChatMe.BussinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private IUnitOfWork unitOfWork;

        public MessageService(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MessageDTO> Create(NewMessageDTO newMessageData) {
            var newMessage = new Message {
                Body = newMessageData.Body,
                UserId = newMessageData.UserId,
                Time = DateTime.Now,
                DialogId = newMessageData.DialogId,
                User = unitOfWork.Users.FindById(newMessageData.UserId)
            };

            unitOfWork.Messages.Create(newMessage);
            await unitOfWork.SaveAsync();

            newMessage = unitOfWork.Messages.Get(newMessage.Id);
            return new MessageDTO {
                Id = newMessage.Id,
                Body = newMessage.Body,
                Time = newMessage.Time,
                AuthorId = newMessage.User.Id,
                Author = newMessage.User.DisplayName
            };
        }

        public IEnumerable<MessageDTO> GetChunk(string userId, int dialogId, int startIndex, int chunkSize) {
            var dialog = unitOfWork.Dialogs.Get(dialogId);
            var messages = dialog.Messages
                .OrderByDescending(m => m.Time)
                .Skip(startIndex)
                .Select(m => new MessageDTO {
                    Id = m.Id,
                    Body = m.Body,
                    Time = m.Time,
                    AuthorId = m.User.Id,
                    Author = m.User.DisplayName
                });

            if (chunkSize != 0) {
                messages = messages.Take(chunkSize);
            }

            return messages;
        }
    }
}
