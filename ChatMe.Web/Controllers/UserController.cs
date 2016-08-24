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

namespace ChatMe.Web.Controllers
{
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Messages()
        {
            var me = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(new MessagesViewModel(me));
        }

        public ActionResult AllPeople(int page = 1)
        {
            const int pageSize = 3;
            var allUsers = unitOfWork.Users.GetAll();

            var usersOnPage = allUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserProfileViewModel
                {
                    AvatarUrl = u.UserInfo.AvatarUrl,
                    UserName = u.UserName,
                    FirstName = u.UserInfo.FirstName,
                    LastName = u.UserInfo.LastName
                });

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allUsers.Count()
            };

            var viewModel = new AllPeopleViewModel
            {
                Users = usersOnPage,
                PageInfo = pageInfo
            };

            return View(viewModel);
        }
    }
}