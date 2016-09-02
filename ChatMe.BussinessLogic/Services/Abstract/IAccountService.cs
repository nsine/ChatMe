using ChatMe.BussinessLogic.DTO;
using Microsoft.AspNet.Identity;
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
