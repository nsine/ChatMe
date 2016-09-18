using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Repositories;
using Microsoft.Owin.Security;
using Ninject.Modules;
using System.Web;

namespace ChatMe.BussinessLogic.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
            Bind<IAuthenticationManager>().ToMethod(c =>
                HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}