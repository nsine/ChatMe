using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.BussinessLogic.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
            Bind<IUserService>().To<IUserService>();
        }
    }
}
