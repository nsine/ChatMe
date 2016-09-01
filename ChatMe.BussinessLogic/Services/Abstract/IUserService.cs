using ChatMe.BussinessLogic.DTO;
using ChatMe.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Services.Abstract
{
    public interface IUserService
    {
        string GetUserDisplayName(User user);
        UserProfileDTO GetUserProfile(string userName, string currectUserId);
        IEnumerable<UserInfoDTO> GetAll();
        IEnumerable<UserInfoDTO> GetAllExceptMe(string userId);
        UserSettingsDTO GetUserSettings(string userId);
        Task<ChangingSettingsResult> ChangeUserSettings(UserSettingsDTO settingsData, Func<string, string> pathResolver);
    }
}
