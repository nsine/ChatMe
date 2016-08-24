using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatMe.DataAccess.Entities
{
    public class UserInfo
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string AboutMe { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }


        public virtual User User { get; set; }
    }
}
