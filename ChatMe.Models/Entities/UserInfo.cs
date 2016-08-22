using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.Models.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //TODO add avatar
        public string AboutMe { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
