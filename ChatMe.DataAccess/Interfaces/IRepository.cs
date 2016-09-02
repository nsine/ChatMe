using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T FindById(object id);
        IQueryable<T> Where(Func<T, Boolean> predicate);
        void Add(T item);
        void Update(T item);
        void Remove(object id);
    }
}