using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Users.Infrastructure {
    public static class IdentityHelpers {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id) {
            var mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType) {
            var fields = typeof(ClaimTypes).GetFields();
            foreach (var field in fields) {
                if (field.GetValue(null).ToString() == claimType) {
                    return new MvcHtmlString(field.Name);
                }
            }

            return new MvcHtmlString(String.Format("{0}", claimType.Split('/', '.').Last()));
        }
    }
}