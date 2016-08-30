using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime Time { get; set; }
        public bool IsMy { get; set; }
    }
}
