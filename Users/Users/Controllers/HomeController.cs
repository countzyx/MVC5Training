using System.Collections.Generic;
using System.Web.Mvc;


namespace Users.Controllers
{
    public class HomeController : Controller {
        public ActionResult Index() {
            var data = new Dictionary<string, object>();
            data.Add("Placeholder", "Placeholder");

            return View(data);
        }
    }
}