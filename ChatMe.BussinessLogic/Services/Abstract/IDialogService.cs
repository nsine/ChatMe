using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IDialogService
    {
        IEnumerable<DialogPreviewDTO> GetChunk(string userId, int startIndex, int chunkSize);
        Task<int> Create(NewDialogDTO data);
        Task<bool> Delete(int dialogId);
        int GetIdByMembers(IEnumerable<string> userIds);
    }
}
