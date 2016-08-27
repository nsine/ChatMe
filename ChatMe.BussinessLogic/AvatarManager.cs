using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChatMe.BussinessLogic
{
    public static class AvatarManager
    {
        public static AvatarInfo GetPath(User user, Func<string, string> pathResolver) {
            var fileName = "default";
            var mimeType = "image/png";

            if (user != null) {
                if (!string.IsNullOrEmpty(user.UserInfo.AvatarFilename)) {
                    fileName = user.UserInfo.AvatarFilename;
                    mimeType = user.UserInfo.AvatarMimeType;
                }
            }
            var dir = pathResolver("/App_Data/Avatars");
            var dirInfo = new DirectoryInfo(dir);
            var file = dirInfo.GetFiles($"{fileName}.*")
                .FirstOrDefault()?.Name;

            return new AvatarInfo {
                Path = Path.Combine(dir, file),
                Type = mimeType
            };
        }
    }
}
