using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string AuthorId { get; set; }
        public bool IsAuthorOnline { get; set; }
        public DateTime Time { get; set; }
        public int DialogId { get; set; }
    }
}