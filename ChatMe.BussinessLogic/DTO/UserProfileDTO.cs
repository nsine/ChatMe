using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.DTO
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string AboutMe { get; set; }
        public bool IsOwner { get; set; }
        public bool IsFollowing { get; set; }
        public string DisplayName { get; set; }
        public bool IsOnline { get; set; }

        public IEnumerable<PostDTO> Posts;

        public string AvatarFilename { get; set; }
        public string AvatarMimeType { get; set; }
    }
}
