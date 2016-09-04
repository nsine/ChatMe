using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Areas.Admin.Models
{
    public class NewUserViewModel : UserViewModel
    {
        public string Password { get; set; }
    }
}