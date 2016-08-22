using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatMe.DataAccess.Entities
{
    public class UserInfo
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //TODO add avatar
        public string AboutMe { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        
        public virtual User User { get; set; }
    }
}
