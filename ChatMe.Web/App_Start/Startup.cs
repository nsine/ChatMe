using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(ChatMe.Startup))]

namespace ChatMe
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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