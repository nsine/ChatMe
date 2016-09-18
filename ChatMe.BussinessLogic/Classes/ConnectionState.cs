using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Classes
{
    public class ConnectionState
    {
        public bool IsNewUser { get; set; }
        public string OldConnectionId { get; set; }
    }
}
