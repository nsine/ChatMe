using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatMe.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
}
