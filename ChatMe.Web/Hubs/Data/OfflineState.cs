using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ChatMe.Web.Hubs.Data
{
    public class OfflineState
    {
        public User User { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
    }
}