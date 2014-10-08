using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;


namespace Users.Controllers
{
    public class HomeController : Controller {
        [Authorize]
        public ActionResult Index() {
            return View(GetData("Index"));
        }


        [Authorize(Roles="Users")]
        public ActionResult OtherAction() {
            return View("Index", GetData("OtherAction"));
        }


        private IDictionary<string, object> GetData(string actionName) {
            var dict = new Dictionary<string, object>();
            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }
    }
}