using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> Users { get; }
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<Post> Posts { get; }
        IRepository<Like> Likes { get; }

        DbContext GetDb();

        void Save();
        Task SaveAsync();
    }
}
