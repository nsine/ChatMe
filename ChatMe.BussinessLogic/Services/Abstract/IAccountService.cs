using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUser(RegistrationInfoDTO data);
        Task<bool> Login(LoginDTO data);
        bool Logout();
    }
}
