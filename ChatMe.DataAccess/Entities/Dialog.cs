using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Entities
{
    public class Dialog
    {
        public int Id { get; set; }

        [Required]
        public string FirstUserId { get; set; }
        public virtual User FirstUser { get; set; }
        [Required]
        public string SecondUserId { get; set; }
        public virtual User SecondUser { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
