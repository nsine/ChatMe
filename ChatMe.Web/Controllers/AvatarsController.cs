using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    public class AvatarsController : Controller
    {
        private IUnitOfWork db;
        private IAvatarService avatarService;

        public AvatarsController(IUnitOfWork unitOfWork, IAvatarService avatarService) {
            db = unitOfWork;
            this.avatarService = avatarService;
        }

        public async Task<ActionResult> GetAvatar(string userId) {
            var user = await db.Users.FindByIdAsync(userId);
            var avatarInfo = avatarService.GetPath(user, Server.MapPath);

            return File(avatarInfo.Path, avatarInfo.Type);
        }
    }
}