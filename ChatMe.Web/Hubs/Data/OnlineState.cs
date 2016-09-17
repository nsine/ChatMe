using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Hubs.Data
{
    public class OnlineState
    {
        public User User { get; set; }
        public string ConnectionId { get; set; }
    }
}