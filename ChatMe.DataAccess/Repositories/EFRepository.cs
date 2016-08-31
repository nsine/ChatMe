using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Repositories
{
    public class EFRepository<T> : IRepository<T>
        where T: class
    {
        private ChatMeContext db;

        public EFRepository(ChatMeContext db) {
            this.db = db;
        }

        public void Create(T item) {
            db.Set<T>().Add(item);
            db.Entry(item).State = EntityState.Added;
        }

        public void Delete(object id) {
            db.Set<T>().Remove(Get(id));
        }

        public IQueryable<T> Find(Func<T, bool> predicate) {
            return db.Set<T>().Where(predicate).AsQueryable();
        }

        public T Get(object id) {
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
