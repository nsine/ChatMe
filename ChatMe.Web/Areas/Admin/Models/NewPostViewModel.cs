using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Areas.Admin.Models
{
    public class NewPostViewModel
    {
        public string UserName { get; set; }
        public string Body { get; set; }
    }
}