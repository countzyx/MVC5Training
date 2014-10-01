using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
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


        [HttpPost]
        public async Task<ActionResult> Delete(string id) {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null) {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                } else {
                    return View("Error", result.Errors);
                }
            } else {
                return View("Error", new string[] { "User Not Found" });
            }
        }


        public async Task<ActionResult> Edit(string id) {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null) {
                return View(user);
            } else {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password) {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null) {
                user.Email = email;
                var validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded) {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != String.Empty) {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded) {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    } else {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null)
                    || (validEmail.Succeeded && password != String.Empty && validPass.Succeeded)) {
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded) {
                        return RedirectToAction("Index");
                    } else {
                        AddErrorsFromResult(result);
                    }
                }
            } else {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
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