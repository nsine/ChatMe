using ChatMe.BussinessLogic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IChatHubService
    {
        IList<string> GetOnlineIds();
        ConnectionState Connect(string userId, string connectionId);
        Task Disconnect(string userId, Action clientCallback);
        Task<IEnumerable<int>> GetUserDialogIds(string userId);
    }
}
