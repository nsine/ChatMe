using System;

namespace ChatMe.Web.Controllers
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public string AvatarUrl { get; set; }
        public int Likes { get; set; }
        public string Author { get; set; }
        public string AuthorLink { get; set; }
        public bool IsLikedByMe { get; set; }
    }
}