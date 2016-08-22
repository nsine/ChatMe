using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatMe.DataAccess.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private ChatMeContext db;

        public MessageRepository(ChatMeContext db)
        {
            this.db = db;
        }

        public void Create(Message item)
        {
            db.Messages.Add(item);
        }

        public void Delete(int id)
        {
            db.Messages.Remove(db.Messages.FirstOrDefault(u => u.Id == id));
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return db.Messages.Where(predicate);
        }

        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Messages;
        }

        public void Update(Message item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
