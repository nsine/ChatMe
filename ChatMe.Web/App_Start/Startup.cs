using Microsoft.Owin;
using Owin;
using ChatMe.DataAccess.Entities;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using ChatMe.DataAccess.EF;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

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
                LoginPath = new PathString("/Account/Login"),
            });

            var unityHubActivator = new MvcHubActivator();

            GlobalHost.DependencyResolver.Register(
                typeof(IHubActivator),
                () => unityHubActivator);

            app.MapSignalR();
        }

        public class MvcHubActivator : IHubActivator
        {
            public IHub Create(HubDescriptor descriptor) {
                return (IHub)DependencyResolver.Current
                    .GetService(descriptor.HubType);
            }
        }
    }
}