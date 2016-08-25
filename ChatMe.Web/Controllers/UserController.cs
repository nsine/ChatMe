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

namespace ChatMe.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork uow) {
            unitOfWork = uow;
        }

        private AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        // GET: User
        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> Messages() {
            var me = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(new MessagesViewModel(me));
        }

        public ActionResult AllUsers(int page = 1) {
            const int pageSize = 3;
            var allUsers = unitOfWork.Users.GetAll();

            var usersOnPage = allUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserProfileViewModel {
                    AvatarFilename = u.UserInfo.AvatarFilename,
                    UserName = u.UserName,
                    FirstName = u.UserInfo.FirstName,
                    LastName = u.UserInfo.LastName
                });

            var pageInfo = new PageInfo {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allUsers.Count()
            };

            var viewModel = new AllUsersViewModel {
                Users = usersOnPage,
                PageInfo = pageInfo
            };

            return View(viewModel);
        }

        public ActionResult Friends() {
            return View();
        }

        [HttpGet]
        public ActionResult Settings() {
            var me = UserManager.FindById(User.Identity.GetUserId());
            var viewModel = new UserSettingsViewModel {
                Id = me.Id,
                Email = me.Email,
                FirstName = me.UserInfo.FirstName,
                LastName = me.UserInfo.LastName,
                AboutMe = me.UserInfo.AboutMe,
                AvatarFilename = me.UserInfo.AvatarFilename,
                Phone = me.UserInfo.Phone,
                Skype = me.UserInfo.Skype
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Settings(UserSettingsViewModel viewModel, HttpPostedFileBase avatar = null) {
            if (ModelState.IsValid) {
                var me = UserManager.FindById(User.Identity.GetUserId());

                var isPassValid = UserManager.CheckPassword(me, viewModel.Password);
                if (!isPassValid) {
                    ModelState.AddModelError("", "Invalid password");
                    viewModel.Password = "";
                    return View(viewModel);
                }

                if (!string.IsNullOrEmpty(viewModel.NewPassword)) {
                    if (viewModel.NewPassword != viewModel.NewPasswordConfirmation) {
                        ModelState.AddModelError("", "Passwords don't match");
                        viewModel.NewPassword = "";
                        viewModel.NewPasswordConfirmation = "";
                        viewModel.Password = "";
                        return View(viewModel);
                    }

                    await UserManager.ChangePasswordAsync(me.Id, viewModel.Password, viewModel.NewPassword);
                }

                me.UserInfo.FirstName = viewModel.FirstName;
                me.UserInfo.LastName = viewModel.LastName;
                me.UserInfo.Phone = viewModel.Phone;
                me.UserInfo.Skype = viewModel.Skype;
                me.Email = viewModel.Email;
                me.UserInfo.AboutMe = viewModel.AboutMe;

                if (avatar != null) {
                    var extension = avatar.ContentType.Split('/')[1];
                    var filename = $"{me.Id}.{extension}";
                    var savePath = Path.Combine(ConfigurationManager.AppSettings["avatarPath"].ToString(),
                        filename);
                    avatar.SaveAs(Server.MapPath(savePath));

                    me.UserInfo.AvatarFilename = filename;
                    me.UserInfo.AvatarMimeType = avatar.ContentType;
                }

                UserManager.Update(me);
                //await unitOfWork.SaveAsync();
                var dialog = new Dialog {
                    Id = 1
                };

                var Message = new Message {
                    Body = "dasfa",
                    DialogId = 1,
                    Time = DateTime.Now
                };

                unitOfWork.Dialogs.Create(dialog);
                unitOfWork.Messages.Create(Message);
                unitOfWork.Save();

                return RedirectToAction("Index");
            } else {
                return View(viewModel);
            }
        }

        public ActionResult GetAvatar(string id) {
            var user = unitOfWork.Users.Get(id);
            var fileName = "default";
            var mimeType = "image/png";

            if (user != null) {
                if (!string.IsNullOrEmpty(user.UserInfo.AvatarFilename)) {
                    fileName = user.UserInfo.AvatarFilename;
                    mimeType = user.UserInfo.AvatarMimeType;
                }
            }
            var dir = Server.MapPath("/App_Data/Avatars");
            var dirInfo = new DirectoryInfo(dir);
            var file = dirInfo.GetFiles($"{fileName}.*")
                .FirstOrDefault()?.Name;

            var path = Path.Combine(dir, file);

            return File(path, mimeType);
        }
    }
}