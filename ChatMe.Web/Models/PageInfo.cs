using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}