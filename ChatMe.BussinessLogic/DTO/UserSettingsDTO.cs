using System.Web;

namespace ChatMe.BussinessLogic.DTO
{
    public class UserSettingsDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string AboutMe { get; set; }

        public string AvatarFilename { get; set; }
        public string AvatarMimeType { get; set; }
        public HttpPostedFileBase Avatar { get; set; }

        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }

        public string Password { get; set; }
    }
}
