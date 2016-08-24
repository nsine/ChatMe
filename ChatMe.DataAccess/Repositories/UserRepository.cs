using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ChatMe.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ChatMeContext db;

        public UserRepository(ChatMeContext db)
        {
            this.db = db;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(object id)
        {
            db.Users.Remove(db.Users.FirstOrDefault(u => u.Id == (string)id));
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate);
        }

        public User Get(object id)
        {
            return db.Users.Find((string)id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
