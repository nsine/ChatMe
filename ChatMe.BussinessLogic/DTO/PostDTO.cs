using System;

namespace ChatMe.BussinessLogic.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public int Likes { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string AuthorUserName { get; set; }
        public bool IsAuthorOnline { get; set; }
        public bool IsLikedByMe { get; set; }
    }
}
