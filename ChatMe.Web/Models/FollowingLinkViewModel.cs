using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class FollowingLinkViewModel
    {
        public string UserId { get; set; }
        public string FollowingUserId { get; set; }
    }
}