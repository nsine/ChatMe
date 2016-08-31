using ChatMe.DataAccess.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<Post> Posts { get; }
        IRepository<Like> Likes { get; }
        IRepository<FollowerLink> FollowerLinks { get; }

        DbContext GetDb();

        void Save();
        Task SaveAsync();
    }
}
