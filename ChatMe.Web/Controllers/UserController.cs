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
using ChatMe.DataAccess.Interfaces;
using System.IO;
using System.Configuration;
using ChatMe.BussinessLogic;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.Web.Controllers.Abstract;
using AutoMapper;
using ChatMe.BussinessLogic.DTO;
using ChatMe.BussinessLogic.Services;

namespace ChatMe.Web.Controllers
{
    [Authorize]
    public class UserController : IdentityController
    {
        private IUnitOfWork unitOfWork;
        private IUserService userService;
        private IAvatarService avatarService;

        public UserController(IUnitOfWork uow, IUserService userService, IAvatarService avatarService) {
            unitOfWork = uow;
            this.userService = userService;
            this.avatarService = avatarService;
        }

        public ActionResult Index() {
            return RedirectToAction("UserProfile", new {
                userName = User.Identity.GetUserName()
            });
        }

        public ActionResult News() {
            var userId = User.Identity.GetUserId();
            return View((object)userId);
        }

        public ActionResult UserProfile(string userName) {
            var userProfileData = userService.GetUserProfile(userName, User.Identity.GetUserId());

            if (userProfileData == null) {
                throw new HttpException(404, "User not found");
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
                .Take(pageSize);

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
                var me = UserManager.FindById(User.Identity.GetUserId());

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

        [Route("avatar/{id}")]
        public ActionResult GetAvatar(string id) {
            var user = unitOfWork.Users.FindById(id);
            var avatarInfo = avatarService.GetPath(user, Server.MapPath);

            return File(avatarInfo.Path, avatarInfo.Type);
        }
    }
}