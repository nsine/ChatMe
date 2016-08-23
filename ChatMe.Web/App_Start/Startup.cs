using Microsoft.Owin;
using Owin;
using ChatMe.DataAccess.Entities;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using ChatMe.DataAccess.EF;

[assembly: OwinStartup(typeof(ChatMe.Startup))]

namespace ChatMe
{
    public class Startup
    {
        public object ApplicationUserManager { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ChatMeContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
            });
        }
    }
}