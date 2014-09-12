using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

using Users.Infrastructure;

namespace Users.Controllers {
    public class AdminController : Controller {
        public ActionResult Index() {
            return View(UserManager.Users);
        }

        public AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}