using ChatMe.Web.Areas.Admin.Controllers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Areas.Admin.Controllers
{
    public class AdminController : AdminAuthorizeController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}