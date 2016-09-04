using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using ChatMe.DataAccess.Interfaces;

namespace ChatMe.BussinessLogic.Services
{
    public class DialogService : IDialogService
    {
        private IUnitOfWork db;
        private IUserService userService;

        public DialogService(IUnitOfWork unitOfWork, IUserService userService) {
            this.db = unitOfWork;
            this.userService = userService;
        }

        public async Task<int> Create(NewDialogDTO data) {
            var users = data.UserIds
                .Select(id => db.Users.FindById(id))
                .ToList();

            var newDialog = new Dialog {
                Users = users,
                CreateTime = DateTime.Now
            };

            db.Dialogs.Add(newDialog);
            await db.SaveChangesAsync();
            return newDialog.Id;
        }

        public async Task<bool> Delete(int dialogId) {
            db.Dialogs.Remove(dialogId);
            await db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<DialogPreviewDTO> GetChunk(string userId, int startIndex, int chunkSize) {
            var me = db.Users.FindById(userId);
            var dialogs = me.Dialogs
                .OrderByDescending(d => (d.LastMessageTime.HasValue ? d.LastMessageTime : d.CreateTime))
                .Skip(startIndex)
                .Select(d => new DialogPreviewDTO {
                    Id = d.Id,
                    LastMessage = d.Messages
                        .OrderByDescending(m => m.Time)
                        .FirstOrDefault()
                        ?.Body,
                    LastMessageAuthor = userService.GetUserDisplayName(d.Messages
                        .OrderByDescending(m => m.Time)
                        .FirstOrDefault()
                        ?.User),
                    Users = d.Users
                        .Where(u => u.Id != userId)
                        .Select(u => new UserInfoDTO {
                            Id = u.Id,
                            FirstName = u.UserInfo.FirstName,
                            LastName = u.UserInfo.LastName,
                            UserName = u.UserName,
                            AvatarFilename = u.UserInfo.AvatarFilename
                        })
                });

            if (chunkSize != 0) {
                dialogs = dialogs.Take(chunkSize);
            }

            return dialogs;
        }

        public int GetIdByMembers(IEnumerable<string> userIds) {
            var matchedDialogs = db.Dialogs.Where(d => d.Users
                .Select(u => u.Id)
                .SequenceEqual(userIds));
            if (matchedDialogs.Count() == 0) {
                return -1;
            } else {
                return matchedDialogs.First().Id;
            }
        }
    }
}
