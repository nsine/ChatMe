using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string AboutMe { get; set; }
        public string RolesString { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string AvatarFilename { get; set; }
        public string AvatarMimeType { get; set; }
        public HttpPostedFileBase Avatar { get; set; }
        public string AvatarPath { get; set; }
    }
}