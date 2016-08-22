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
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Users.GetAll()).Returns(new List<User>()
            {
                new User { Email = "gh@gmail.com", Password = "fadjs" }
            });

            kernel.Bind<IUnitOfWork>().ToConstant(mock.Object);
        }
    }
}