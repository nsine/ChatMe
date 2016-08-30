using ChatMe.BussinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class ChangingSettingsResult
    {
        public bool Succeeded { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();
        public UserSettingsDTO Settings { get; set; }
    }
}
