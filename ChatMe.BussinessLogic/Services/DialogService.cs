using ChatMe.BussinessLogic.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.DTO;

namespace ChatMe.BussinessLogic.Services
{
    public class DialogService : IDialogService
    {
        public bool CreateNewDialog(NewDialogDTO data) {
            throw new NotImplementedException();
        }

        public bool Delete(int dialogId) {
            throw new NotImplementedException();
        }

        public IEnumerable<DialogPreviewDTO> GetChunk(string userId, int startIndex, int chunkSize) {
            throw new NotImplementedException();
        }
    }
}
