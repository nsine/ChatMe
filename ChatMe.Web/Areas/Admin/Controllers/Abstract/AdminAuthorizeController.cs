using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Areas.Admin.Controllers.Abstract
{
    [Authorize(Roles = "admin")]
    public class AdminAuthorizeController : Controller { }
}