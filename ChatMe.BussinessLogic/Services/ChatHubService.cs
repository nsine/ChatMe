using ChatMe.BussinessLogic.Classes;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services
{
    public class ChatHubService : IChatHubService
    {
        private IUnitOfWork db;

        private ICollection<OnlineState> onlineUsers = new List<OnlineState>();
        private ICollection<OfflineState> pendingOffline = new List<OfflineState>();

        public ChatHubService(IUnitOfWork unitOfWork) {
            db = unitOfWork;
        }

        public IList<string> GetOnlineIds() {
            return onlineUsers.Select(u => u.ConnectionId).ToList();
        }

        public async Task MakeOnline(User user, string connectionId) {
            user.IsOnline = true;
            try {
                await db.Users.UpdateAsync(user);
                await db.SaveChangesAsync();
            } catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            onlineUsers.Add(new OnlineState {
                User = user,
                ConnectionId = connectionId
            });
        }

        public IEnumerable<int> GetUserDialogIds(string userId) {
            return db.Users
                .FindById(userId)
                .Dialogs
                .Select(d => d.Id);
        }

        public async Task<bool> Connect(string userId, string connectionId) {
            Debug.Print($"{userId} {connectionId} connected");
            var offlineState = pendingOffline
                .Where(s => s.User.Id == userId)
                .FirstOrDefault();

            // If it's new user
            if (offlineState == null) {
                var user = await db.Users.FindByIdAsync(userId);
                await MakeOnline(user, connectionId);
                return true;
            } else {
                offlineState.CancelTokenSource.Cancel();
                return false;
            }
        }

        public async Task Disconnect(string userId, Action clientCallback) {
            Debug.Print($"{userId} disconnected");
            var user = await db.Users.FindByIdAsync(userId);
            await MakeOffline(user);
        }

        private Task MakeOffline(User user) {
            var state = new OfflineState {
                CancelTokenSource = new CancellationTokenSource(),
                User = user
            };

            pendingOffline.Add(state);

            return Task.Factory.StartNew(async () => {
                // Time in seconds before user will be disconnected
                const int waitTime = 3;

                for (int i = 0; i < waitTime; i++) {
                    Thread.Sleep(1000);
                    if (state.CancelTokenSource.Token.IsCancellationRequested) {
                        pendingOffline.Remove(state);
                        return;
                    }
                }

                user.IsOnline = false;
                onlineUsers.Remove(onlineUsers.Where(s => s.User.Id == user.Id).FirstOrDefault());
                try {
                    pendingOffline.Remove(state);
                    await db.Users.UpdateAsync(state.User);
                    await db.SaveChangesAsync();
                } catch (DbEntityValidationException e) {
                    foreach (var eve in e.EntityValidationErrors) {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors) {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }, state.CancelTokenSource.Token);
        }
    }
}
