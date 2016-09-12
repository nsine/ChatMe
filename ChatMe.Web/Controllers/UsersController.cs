using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using ChatMe.Web.Models;
using ChatMe.DataAccess.Interfaces;
using ChatMe.BussinessLogic.Services.Abstract;
using AutoMapper;
using ChatMe.BussinessLogic.DTO;

namespace ChatMe.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IUserService userService;

        public UsersController(IUnitOfWork uow, IUserService userService) {
            unitOfWork = uow;
            this.userService = userService;
        }

        public ActionResult Index() {
            return RedirectToRoute("UserProfile", new {
                userName = User.Identity.GetUserName()
            });
        }

        public ActionResult News() {
            var userId = User.Identity.GetUserId();
            return View((object)userId);
        }

        public ActionResult UserProfile(string userName) {
            if (userName == null) {
                var myName = User.Identity.GetUserName();
                return RedirectToRoute("UserProfile", new { userName = myName });
            }

            var userProfileData = userService.GetUserProfile(userName, User.Identity.GetUserId());

            if (userProfileData == null) {
                return HttpNotFound("User not found");
            }

            Mapper.Initialize(cfg => {
                cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
            });

            var userProfile = Mapper.Map<UserProfileViewModel>(userProfileData);
            return View(userProfile);
        }

        public ActionResult Messages(int? dialogId) {
            var viewModel = new DialogInitViewModel {
                DialogId = dialogId,
                UserId = User.Identity.GetUserId()
            };

            return View(viewModel);
        }

        public ActionResult AllUsers(int page = 1) {
            const int pageSize = 20;
            var allUsersData = userService.GetAllExceptMe(User.Identity.GetUserId());

            var usersOnPageData = allUsersData
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            Mapper.Initialize(cfg => cfg.CreateMap<UserInfoDTO, UserPreviewViewModel>());
            var usersOnPage = Mapper.Map<IEnumerable<UserPreviewViewModel>>(usersOnPageData);

            var pageInfo = new PageInfo {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allUsersData.Count()
            };

            var viewModel = new AllUsersViewModel {
                Users = usersOnPage,
                PageInfo = pageInfo
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Settings() {
            var userSettingsData = userService.GetUserSettings(User.Identity.GetUserId());

            Mapper.Initialize(cfg => cfg.CreateMap<UserSettingsDTO, UserSettingsViewModel>());
            var userSettings = Mapper.Map<UserSettingsViewModel>(userSettingsData);

            return View(userSettings);
        }

        [HttpPost]
        public async Task<ActionResult> Settings(UserSettingsViewModel viewModel, HttpPostedFileBase avatar = null) {

            if (ModelState.IsValid) {
                var me = unitOfWork.Users.FindById(User.Identity.GetUserId());

                Mapper.Initialize(cfg => cfg.CreateMap<UserSettingsViewModel, UserSettingsDTO>());
                var userSettingsData = Mapper.Map<UserSettingsDTO>(viewModel);
                userSettingsData.Avatar = avatar;

                var result = await userService.ChangeUserSettings(userSettingsData, Server.MapPath);
                if (!result.Succeeded) {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }

                    Mapper.Initialize(cfg => cfg.CreateMap<UserSettingsDTO, UserSettingsViewModel>());
                    var userSettings = Mapper.Map<UserSettingsViewModel>(result.Settings);

                    return View(userSettings);
                } else {
                    return RedirectToAction("Index");
                }
            } else {
                return View(viewModel);
            }
        }
    }
}