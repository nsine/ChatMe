using ChatMe.DataAccess.Entities;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        void Save();
    }
}
