using System.Web.Security;

using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Infrastructure.Concrete {
    public class FormsAuthProvider : IAuthProvider {
        public bool Authenticate(string username, string password) {
            bool isAuthenticated = FormsAuthentication.Authenticate(username, password);
            if (isAuthenticated) {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return isAuthenticated;
        }
    }
}