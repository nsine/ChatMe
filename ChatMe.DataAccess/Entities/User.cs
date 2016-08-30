using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatMe.DataAccess.Entities
{
    public class User : IdentityUser
    {
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
    }
}
