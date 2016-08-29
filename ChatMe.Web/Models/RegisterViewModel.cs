using System;
using System.ComponentModel.DataAnnotations;

namespace ChatMe.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}