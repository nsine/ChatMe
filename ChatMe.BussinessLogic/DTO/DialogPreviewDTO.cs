using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class DialogPreviewDTO
    {
        public int Id { get; set; }
        public string LastMessage { get; set; }
        public string LastMessageAuthor { get; set; }
        public IEnumerable<UserInfoDTO> Users { get; set; }
    }
}
