using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class UserPreviewViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarFilename { get; set; }
        public bool IsOnline { get; set; }
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