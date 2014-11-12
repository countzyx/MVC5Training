using System;
using System.Linq;
using System.Web.Mvc;

using SimpleApp.Models;

namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Color color) {
            Color? oldColor = Session["color"] as Color?;
            if (oldColor != null) {
                Votes.ChangeVote(color, (Color)oldColor);
            } else {
                Votes.RecordVote(color);
            }
            ViewBag.SelectedColor = Session["color"] = color;
            return View();
        }

        public ActionResult Modules() {
            var modules = HttpContext.ApplicationInstance.Modules;
            var data = modules.AllKeys
                .Select(m => new Tuple<string, string>(
                    m.StartsWith("__Dynamic") ? m.Split('_', ',')[3] : m, modules[m].GetType().Name))
                .OrderBy(m => m.Item1)
                .ToArray();

            return View(data);
        }
    }
}