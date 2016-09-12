using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Find(object id);
        Task<T> FindAsync(object id);
        IQueryable<T> Where(Func<T, Boolean> predicate);
        void Add(T item);
        void Update(T item);
        void Remove(object id);
    }
}