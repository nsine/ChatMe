using System;
using System.ComponentModel.DataAnnotations;

namespace ChatMe.DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
