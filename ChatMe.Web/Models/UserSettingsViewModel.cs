using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class UserSettingsViewModel
    {
        public string Id { get; set; }
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
        
        public string AvatarFilename { get; set; }
        public string AvatarMimeType { get; set; }
        public byte[] Avatar { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string NewPasswordConfirmation { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}