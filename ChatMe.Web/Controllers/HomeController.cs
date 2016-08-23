using ChatMe.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        [Authorize]
        public ActionResult Index()
        {
            var users = unitOfWork.Users.GetAll();
            return View(users);
        }
    }
}