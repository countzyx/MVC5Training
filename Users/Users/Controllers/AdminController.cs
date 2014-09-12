﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Users.Infrastructure;
using Users.Models;
using Users.Models.UserViewModels;


namespace Users.Controllers {
    public class AdminController : Controller {
        public ActionResult Index() {
            return View(UserManager.Users);
        }


        public ActionResult Create() {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model) {
            if (ModelState.IsValid) {
                var user = new AppUser {
                    UserName = model.Name,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                } else {
                    AddErrorsFromResult(result);
                }
            }

            return View(model);
        }


        private void AddErrorsFromResult(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }


        public AppUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}