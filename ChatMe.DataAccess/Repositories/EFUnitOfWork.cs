using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using ChatMe.DataAccess.Identity;

namespace ChatMe.DataAccess.Repositories
{
    public sealed class EFUnitOfWork : IUnitOfWork
    {
        private ChatMeContext db;

        private EFRepository<Message> messageRepo;
        private UserManager<User> userRepo;
        private RoleManager<Role> roleRepo;
        private EFRepository<Dialog> dialogRepo;
        private EFRepository<Post> postRepo;
        private EFRepository<Like> likeRepo;

        public EFUnitOfWork()
        {
            db = new ChatMeContext();
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepo == null) {
                    messageRepo = new EFRepository<Message>(db);
                }
                return messageRepo;
            }
        }

        UserManager<User> IUnitOfWork.Users {
            get {
                if (userRepo == null) {
                    userRepo = new AppUserManager(new UserStore<User>(db));
                }

                return userRepo;
            }
        }

        public IRepository<Dialog> Dialogs
        {
            get
            {
                if (dialogRepo == null) {
                    dialogRepo = new EFRepository<Dialog>(db);
                }
                return dialogRepo;
            }
        }

        public IRepository<Post> Posts {
            get {
                if (postRepo == null) {
                    postRepo = new EFRepository<Post>(db);
                }
                return postRepo;
            }
        }

        public IRepository<Like> Likes {
            get {
                if (likeRepo == null) {
                    likeRepo = new EFRepository<Like>(db);
                }
                return likeRepo;
            }
        }

        public RoleManager<Role> Roles {
            get {
                if (roleRepo == null) {
                    roleRepo = new AppRoleManager(new RoleStore<Role>(db));
                }

                return roleRepo;
            }
        }

        public void SaveChanges()
        {
            var a = db.SaveChanges();
            a = 5;
        }

        public async Task SaveChangesAsync() {
            await db.SaveChangesAsync();
        }

        public void Dispose() {
            userRepo?.Dispose();
            db.Dispose();
        }

        public DbContext GetDb() {
            return db;
        }
    }
}
