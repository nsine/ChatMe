using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IMessageService
    {
        IEnumerable<MessageDTO> GetChunk(string userId, int dialogId, int startIndex, int chunkSize);
        Task<bool> Create(NewMessageDTO newMessageData);
    }
}
