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
    class DialogRepository : IRepository<Dialog>
    {
        private ChatMeContext db;

        public DialogRepository(ChatMeContext db)
        {
            this.db = db;
        }

        public void Create(Dialog item)
        {
            db.Dialogs.Add(item);
        }

        public void Delete(object id)
        {
            db.Dialogs.Remove(db.Dialogs.FirstOrDefault(d => d.Id == (int)id));
        }

        public IEnumerable<Dialog> Find(Func<Dialog, bool> predicate)
        {
            return db.Dialogs.Where(predicate);
        }

        public Dialog Get(object id)
        {
            return db.Dialogs.Find((int)id);
        }

        public IEnumerable<Dialog> GetAll()
        {
            return db.Dialogs;
        }

        public void Update(Dialog item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
