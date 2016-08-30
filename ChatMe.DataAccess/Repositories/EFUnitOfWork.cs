using System;
using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ChatMeContext db;

        private EFRepository<Message> messageRepo;
        private EFRepository<User> userRepo;
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

        public IRepository<User> Users
        {
            get
            {
                if (userRepo == null) {
                    userRepo = new EFRepository<User>(db);
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

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync() {
            await db.SaveChangesAsync();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}
