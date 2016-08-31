using ChatMe.DataAccess.Entities;
using ChatMe.Web.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Skype { get; set; }
        [DisplayName("About me")]
        public string AboutMe { get; set; }
        public bool IsOwner { get; set; }
        public bool IsFollowing { get; set; }
        public string DisplayName { get; set; }


        public IEnumerable<PostViewModel> Posts;

        public string AvatarFilename { get; set; }
        public string AvatarMimeType { get; set; }
    }
}