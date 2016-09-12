using System;

namespace ChatMe.Web.Areas.Admin.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Time { get; set; }
    }
}