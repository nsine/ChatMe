using ChatMe.BussinessLogic.Services;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Repositories;
using Microsoft.Owin.Security;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;

namespace ChatMe.BussinessLogic.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
            Bind<IUserService>().To<IUserService>();
            Bind<IActivityService>().To<ActivityService>();
            Bind<IAuthenticationManager>().ToMethod(c =>
                HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}
