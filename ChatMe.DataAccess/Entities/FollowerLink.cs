using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Entities
{
    public class FollowerLink
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FollowingUserId { get; set; }
        public User FollowingUser { get; set; }
    }
}
