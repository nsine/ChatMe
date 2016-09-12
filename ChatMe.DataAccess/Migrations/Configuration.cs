namespace ChatMe.DataAccess.Migrations
{
    using EF;
    using Entities;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ChatMe.DataAccess.EF.ChatMeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ChatMe.DataAccess.EF.ChatMeContext context)
        {
            var roleManager = new AppRoleManager(new RoleStore<Role>(context));
            var userManager = new AppUserManager(new UserStore<User>(context));

            if (!roleManager.RoleExists("admin")) {
                roleManager.Create(new Role { Name = "admin" });
            }
            if (!roleManager.RoleExists("user")) {
                roleManager.Create(new Role { Name = "user" });
            }

            if (userManager.FindByName("admin") == null) {
                var admin = new User {
                    UserName = "admin",
                    Email = "admin@adminemail.com",
                    UserInfo = new UserInfo {
                        RegistrationDate = DateTime.Now
                    }
                };
                var adminPassword = "adminpass";
                userManager.Create(admin, adminPassword);
                userManager.AddToRole(admin.Id, "admin");
            }
        }
    }
}
