using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public int LikesCount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<User> LikedUsers { get; set; }
    }
}
