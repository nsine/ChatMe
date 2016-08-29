using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class LoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}
