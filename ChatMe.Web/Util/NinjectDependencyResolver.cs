using ChatMe.BussinessLogic.Services;
using ChatMe.BussinessLogic.Services.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
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
            kernel.Bind<IDialogService>().To<DialogService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IActivityService>().To<ActivityService>();
            kernel.Bind<IAvatarService>().To<AvatarService>();
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}