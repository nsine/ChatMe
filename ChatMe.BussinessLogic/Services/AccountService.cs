using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatMe.BussinessLogic.Services.Abstract;

namespace ChatMe.BussinessLogic.Services
{
    public class AccountService : IAccountService
    {
        public async Task<IdentityResult> CreateUser(RegistrationInfoDTO data, AppUserManager userManager) {
            var user = new User {
                UserName = data.UserName,
                Email = data.Email,
                UserInfo = new UserInfo {
                    RegistrationDate = DateTime.Now,
                    FirstName = data.FirstName
                }
            };

            return await userManager.CreateAsync(user, data.Password);
        }

        public async Task<bool> Login
            (LoginDTO data, AppUserManager userManager, IAuthenticationManager authManager) {

            var user = await userManager.FindAsync(data.Login, data.Password);

            if (user == null) {
                return false;
            } else {
                var claim = await userManager
                    .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                Logout(authManager);
                authManager.SignIn(new AuthenticationProperties {
                    IsPersistent = !data.RememberMe.GetValueOrDefault()
                }, claim);

                return true;
            }
        }

        public bool Logout(IAuthenticationManager authManager) {
            authManager.SignOut();
            return true;
        }
    }
}
