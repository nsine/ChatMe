using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string DisplayName {
            get
            {
                if (FirstName != null && LastName != null) {
                    return $"{FirstName} {LastName}";
                } else {
                    return UserName;
                }
            }
        }

    }
}