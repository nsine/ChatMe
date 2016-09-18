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

        private void MakeOnline(User user, string connectionId) {
            user.IsOnline = true;
            db.Users.Update(user);
            db.SaveChanges();

            onlineUsers.Add(new OnlineState {
                User = user,
                ConnectionId = connectionId
            });
        }

        public async Task<IEnumerable<int>> GetUserDialogIds(string userId) {
            var dialogs = (await db.Users
                .FindByIdAsync(userId))
                .Dialogs
                .Select(d => d.Id);
            return dialogs.ToList();
        }

        public ConnectionState Connect(string userId, string connectionId) {
            Debug.Print($"{userId} {connectionId} connected");
            var result = new ConnectionState();

            var offlineState = pendingOffline
                .Where(s => s.User.Id == userId)
                .FirstOrDefault();

            // If it's new user
            if (offlineState == null) {
                var user = db.Users.FindById(userId);
                MakeOnline(user, connectionId);
                result.IsNewUser = true;
            } else {
                offlineState.CancelTokenSource.Cancel();

                var currentOnlineState = onlineUsers
                    .Where(s => s.User.Id == userId)
                    .FirstOrDefault();
                result.IsNewUser = false;
                result.OldConnectionId = currentOnlineState?.ConnectionId;
                // Update connection id
                currentOnlineState.ConnectionId = connectionId;
            }

            return result;
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
                const int waitTime = 10;

                for (int i = 0; i < waitTime; i++) {
                    Thread.Sleep(1000);
                    if (state.CancelTokenSource.Token.IsCancellationRequested) {
                        pendingOffline.Remove(state);
                        return;
                    }
                }

                user.IsOnline = false;
                onlineUsers.Remove(onlineUsers.Where(s => s.User.Id == user.Id).FirstOrDefault());
                pendingOffline.Remove(state);
                Debug.Print("user disconnected");
                await db.Users.UpdateAsync(state.User);
                await db.SaveChangesAsync();
                
            }, state.CancelTokenSource.Token);
        }
    }
}
