using ChatMe.BussinessLogic.Services;
using ChatMe.BussinessLogic.Services.Abstract;
using ChatMe.DataAccess.Entities;
using ChatMe.DataAccess.Interfaces;
using ChatMe.DataAccess.Repositories;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object IUnitOfWork { get; private set; }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        private void AddBindings()
        {
            //kernel.Bind<IUnitOfWork>().ToConstant(mock.Object);
            kernel.Bind<IUnitOfWork>().To<EFUnitOfWork>();
            kernel.Bind<IDialogService>().To<DialogService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IActivityService>().To<ActivityService>();
        }
    }
}