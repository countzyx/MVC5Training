using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Text;

using Users.Models;

namespace Users.Infrastructure {
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext> {
        public void PerformInitialSetup(AppIdentityDbContext context) {
            var userMgr = new AppUserManager(new UserStore<AppUser>(context));
            var roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            var roleName = "Administrators";
            var userName = "Admin";
            var password = "Admin1";
            var email = "admin@example.com";

            if (!roleMgr.RoleExists(roleName)) {
                roleMgr.Create(new AppRole(roleName));
            }

            var user = userMgr.FindByName(userName);
            if (user == null) {
                var result = userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
                if (result.Succeeded) {
                    user = userMgr.FindByName(userName);
                } else {
                    var errorSB = new StringBuilder();
                    foreach (var error in result.Errors) {
                        errorSB.AppendLine(error);
                    }

                    throw new Exception(errorSB.ToString());
                }
            }

            if (!userMgr.IsInRole(user.Id, roleName)) {
                userMgr.AddToRole(user.Id, roleName);
            }
        }
        
        protected override void Seed(AppIdentityDbContext context) {
            PerformInitialSetup(context);
            base.Seed(context);
        }
    }
}