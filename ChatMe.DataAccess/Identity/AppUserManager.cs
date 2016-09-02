using ChatMe.DataAccess.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Ninject;

namespace ChatMe.DataAccess.Entities
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store) { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                                IOwinContext context)
        {
            var kernel = context.Get<StandardKernel>();


            ChatMeContext db = context.Get<ChatMeContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}
