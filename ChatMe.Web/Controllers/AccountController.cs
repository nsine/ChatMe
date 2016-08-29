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

using ChatMe.Web.Models;
using ChatMe.Web.Controllers.Abstract;
using ChatMe.BussinessLogic.Services;
using AutoMapper;
using ChatMe.BussinessLogic.DTO;

namespace ChatMe.Web.Controllers
{
    public class AccountController : IdentityController
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                Mapper.Initialize(cfg => cfg.CreateMap<RegisterViewModel, RegistrationInfoDTO>());
                var regInfo = Mapper.Map<RegistrationInfoDTO>(model);
                var result = await AccountService.CreateUser(regInfo, UserManager);

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
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid) {
                var authManager = HttpContext.GetOwinContext().Authentication;

                Mapper.Initialize(cfg => cfg.CreateMap<LoginViewModel, LoginDTO>());
                var loginData = Mapper.Map<LoginDTO>(model);

                var isSuccessLogin = await AccountService.Login(loginData, UserManager, authManager);

                if (isSuccessLogin) {
                    if (string.IsNullOrEmpty(returnUrl)) {
                        return RedirectToAction("Index", "Home");
                    } else {
                        return Redirect(returnUrl);
                    }
                } else {
                    ModelState.AddModelError("", "Invalid login or password");                    
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            AccountService.Logout(authManager);
            return RedirectToAction("Login");
        }
    }
}