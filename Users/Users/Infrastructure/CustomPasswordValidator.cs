using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace Users.Infrastructure {
    public class CustomPasswordValidator : PasswordValidator {
        public override async Task<IdentityResult> ValidateAsync(string password) {
            var result = await base.ValidateAsync(password);
            if (password.Contains("12345")) {
                var errors = result.Errors.ToList();
                errors.Add("Passwords cannot contain numeric sequences.");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}