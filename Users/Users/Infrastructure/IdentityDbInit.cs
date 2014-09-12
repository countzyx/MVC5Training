using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace Users.Infrastructure {
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext> {
        public void PerformInitialSetup(AppIdentityDbContext context) {

        }
        
        protected override void Seed(AppIdentityDbContext context) {
            PerformInitialSetup(context);
            base.Seed(context);
        }
    }
}