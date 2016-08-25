using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatMe.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
