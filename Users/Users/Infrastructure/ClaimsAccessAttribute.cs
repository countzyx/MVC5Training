using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Users.Infrastructure {
    public class ClaimsAccessAttribute : AuthorizeAttribute {
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }

        protected override bool AuthorizeCore(HttpContextBase context) {
            return context.User.Identity.IsAuthenticated && context.User.Identity is ClaimsIdentity
                && ((ClaimsIdentity)context.User.Identity).HasClaim(c => c.Issuer == Issuer && c.Type == ClaimType && c.Value == Value);
        }
    }
}