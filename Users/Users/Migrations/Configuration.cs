using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Text;

using Users.Infrastructure;
using Users.Models;

namespace Users.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Users.Infrastructure.AppIdentityDbContext>
    {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Users.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(Users.Infrastructure.AppIdentityDbContext context) {
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

            foreach (var dbUser in userMgr.Users) {
                if (dbUser.Country == Countries.NONE) {
                    dbUser.SetCountryFromCity(dbUser.City);
                }
            }

            context.SaveChanges();
        }
    }
}
