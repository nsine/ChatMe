using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMe.DataAccess.Identity
{
    public class AppRoleManager : RoleManager<Role>
    {
        public AppRoleManager(IRoleStore<Role, string> store) : base(store) {
            SeedRoles();           
        }

        private void SeedRoles() {
            if (!this.RoleExists("admin")) {
                this.Create(new Role { Name = "admin" });
            }
            if (!this.RoleExists("user")) {
                this.Create(new Role { Name = "user" });
            }
        }
    }
}
