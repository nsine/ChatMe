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
using Microsoft.Owin.Security;
using ChatMe.Web.Models;

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
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

        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid) {
                var user = await UserManager.FindAsync(model.Login, model.Password);

                if (user == null) {
                    ModelState.AddModelError("", "Invalid login or password");
                } else {
                    var claim = await UserManager
                        .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = !model.RememberMe.GetValueOrDefault()
                    }, claim);

                    if (string.IsNullOrEmpty(returnUrl)) {
                        return RedirectToAction("Index", "Home");
                    } else {
                        return Redirect(returnUrl);
                    }
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}