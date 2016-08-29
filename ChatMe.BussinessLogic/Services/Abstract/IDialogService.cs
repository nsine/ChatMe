using ChatMe.BussinessLogic.DTO;
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
        bool CreateNewDialog(NewDialogDTO data);
        bool Delete(int dialogId);
    }
}
