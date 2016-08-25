using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
