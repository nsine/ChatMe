using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using ChatMe.Web.Models;

namespace ChatMe.Web.Controllers
{
    public class UserController : Controller
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Messages()
        {
            var me = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(new MessagesViewModel(me));
        }
    }
}