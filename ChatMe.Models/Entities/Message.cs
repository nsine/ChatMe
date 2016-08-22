using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public int UserFromId { get; set; }
        public virtual User UserFrom { get; set; }

        public int UserToId { get; set; }
        public virtual User UserTo { get; set; }
    }
}
