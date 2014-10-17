using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Users.Controllers
{
    public class ClaimsController : Controller {
        [Authorize]
        public ActionResult Index() {
            var id = HttpContext.User.Identity as ClaimsIdentity;
            if (id == null) {
                return View("Error", new string[] { "No claims available" });
            } else {
                return View(id.Claims);
            }
        }


        [Authorize(Roles = "DCStaff")]
        public string OtherAction() {
            return "This is the protected action.";
        }
    }
}