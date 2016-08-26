using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Repositories
{
    class PostRepository : IRepository<Post>
    {
        private ChatMeContext db;

        public PostRepository(ChatMeContext db) {
            this.db = db;
        }

        public void Create(Post item) {
            db.Posts.Add(item);
        }

        public void Delete(object id) {
            db.Posts.Remove(db.Posts.FirstOrDefault(d => d.Id == (int)id));
        }

        public IEnumerable<Post> Find(Func<Post, bool> predicate) {
            return db.Posts.Where(predicate);
        }

        public Post Get(object id) {
            return db.Posts.Find((int)id);
        }

        public IEnumerable<Post> GetAll() {
            return db.Posts;
        }

        public void Update(Post item) {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
