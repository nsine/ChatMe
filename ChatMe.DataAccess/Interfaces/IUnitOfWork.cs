using ChatMe.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<Post> Posts { get; }

        void Save();
        Task SaveAsync();
    }
}
