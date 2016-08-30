using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class NewMessageDTO
    {
        public int DialogId { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
    }
}
