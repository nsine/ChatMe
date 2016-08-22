using System;
using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;

namespace ChatMe.DataAccess.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ChatMeContext db;
        private MessageRepository messageRepo;
        private UserRepository userRepo;

        public EFUnitOfWork()
        {
            db = new ChatMeContext();
        }

        IRepository<Message> IUnitOfWork.Messages
        {
            get
            {
                if (messageRepo == null) {
                    messageRepo = new MessageRepository(db);
                }
                return messageRepo;
            }
        }

        IRepository<User> IUnitOfWork.Users
        {
            get
            {
                if (userRepo == null) {
                    userRepo = new UserRepository(db);
                }
                return userRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
