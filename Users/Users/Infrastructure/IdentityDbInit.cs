using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Text;

using Users.Models;

namespace Users.Infrastructure {
    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext> {
    }
}