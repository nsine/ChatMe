using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Models
{
    public class AllPeopleViewModel
    {
        public IEnumerable<UserProfileViewModel> Users { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}