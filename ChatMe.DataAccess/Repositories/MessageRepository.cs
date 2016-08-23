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

        public void Delete(object id)
        {
            db.Messages.Remove(db.Messages.FirstOrDefault(u => u.Id == (int)id));
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return db.Messages.Where(predicate);
        }

        public Message Get(object id)
        {
            return db.Messages.Find((int)id);
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
