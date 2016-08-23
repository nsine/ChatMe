using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ChatMe.Models;
using Microsoft.AspNet.Identity;

namespace ChatMe.Web.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid) {
                var user = new User {
                    UserName = model.UserName,
                    Email = model.Email,
                    UserInfo = new UserInfo
                    {
                        RegistrationDate = DateTime.Now
                    }
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return RedirectToAction("Login", "Account");
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
    }
}