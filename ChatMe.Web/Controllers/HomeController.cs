using System.Web.Mvc;

namespace ChatMe.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Users");
        }
    }
}