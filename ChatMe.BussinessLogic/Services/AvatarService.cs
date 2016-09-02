using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace ChatMe.BussinessLogic.Services
{
    public class AvatarService : IAvatarService
    {
        private IUnitOfWork db;

        public AvatarService(IUnitOfWork unitOfWork) {
            db = unitOfWork;
        }

        public AvatarInfo GetPath(User user, Func<string, string> pathResolver) {
            var fileName = "default";
            var mimeType = "image/png";

            if (user != null) {
                if (!string.IsNullOrEmpty(user.UserInfo.AvatarFilename)) {
                    fileName = user.UserInfo.AvatarFilename;
                    mimeType = user.UserInfo.AvatarMimeType;
                }
            }
            var dir = pathResolver("~/App_Data/Avatars");
            var dirInfo = new DirectoryInfo(dir);
            var file = dirInfo.GetFiles($"{fileName}.*")
                .FirstOrDefault()?.Name;

            return new AvatarInfo {
                Path = Path.Combine(dir, file),
                Type = mimeType
            };
        }

        public AvatarInfo GetPath(string userId) {
            var user = db.Users.FindById(userId);
            Func<string, string> resolver = s => {
                return Path.Combine(HostingEnvironment.ApplicationPhysicalPath, s);
            };

            return GetPath(user, resolver);
        }
    }
}
