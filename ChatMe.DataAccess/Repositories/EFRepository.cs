using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace ChatMe.DataAccess.Repositories
{
    public class EFRepository<T> : IRepository<T>
        where T: class
    {
        private ChatMeContext db;

        public EFRepository(ChatMeContext db) {
            this.db = db;
        }

        public void Add(T item) {
            db.Set<T>().Add(item);
        }

        public void Remove(object id) {
            db.Set<T>().Remove(FindById(id));
        }

        public IQueryable<T> Where(Func<T, bool> predicate) {
            return db.Set<T>().Where(predicate).AsQueryable();
        }

        public T FindById(object id) {
            return db.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll() {
            return db.Set<T>();
        }

        public void Update(T item) {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
