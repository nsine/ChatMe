using ChatMe.Web.Hubs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMe.Web.Hubs
{
    public class ChatHubData
    {
        public ICollection<OnlineState> OnlineUsers { get; set; } = new List<OnlineState>();
        public ICollection<OfflineState> PendingOffline { get; set; } = new List<OfflineState>();
    }
}