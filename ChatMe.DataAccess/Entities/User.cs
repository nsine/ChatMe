using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatMe.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [RegularExpression("^[a-z0-9_-]{3,16}$")]
        public override string UserName { get; set; }

        [NotMapped]
        public string DisplayName {
            get {
                if (UserInfo.FirstName != null && UserInfo.LastName != null) {
                    return $"{UserInfo.FirstName} {UserInfo.LastName}";
                } else {
                    return UserName;
                }
            }
        }

        [Required]
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> FollowingUsers { get; set; }
    }
}
