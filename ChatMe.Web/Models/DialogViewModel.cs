using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class DialogViewModel
    {
        public string Author { get; set; }
        public string LastMessageSnippet { get; set; }
    }
}