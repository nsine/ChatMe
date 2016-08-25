using ChatMe.DataAccess.Entities;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }

        void Save();
        Task SaveAsync();
    }
}
