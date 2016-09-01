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
using ChatMe.DataAccess.Interfaces;

namespace ChatMe.BussinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork db;
        private IAuthenticationManager authManager;

        public AccountService(IUnitOfWork db, IAuthenticationManager authManager) {
            this.db = db;
            this.authManager = authManager;
        }

        public async Task<IdentityResult> CreateUser(RegistrationInfoDTO data) {
            var user = new User {
                UserName = data.UserName,
                Email = data.Email,
                UserInfo = new UserInfo {
                    RegistrationDate = DateTime.Now,
                    FirstName = data.FirstName
                }
            };

            return await db.Users.CreateAsync(user, data.Password);
        }

        public async Task<bool> Login(LoginDTO data) {

            var user = await db.Users.FindAsync(data.Login, data.Password);

            if (user == null) {
                return false;
            } else {
                var claim = await db.Users
                    .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                Logout();
                authManager.SignIn(new AuthenticationProperties {
                    IsPersistent = !data.RememberMe
                }, claim);

                return true;
            }
        }

        public bool Logout() {
            authManager.SignOut();
            return true;
        }
    }
}
