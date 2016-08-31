using System;
using System.ComponentModel.DataAnnotations;

namespace ChatMe.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("^[a-z0-9_-]+$", ErrorMessage = "Login must contain only letters, digits, underscore or minus symbols")]
        [MaxLength(16, ErrorMessage = "Login should be shorter than 16 symbols")]
        [MinLength(3, ErrorMessage = "Login length should be at least 3 symbols")]
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