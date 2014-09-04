using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using Moq;

using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests {
    /// <summary>
    /// Summary description for AdminSecurityTests
    /// </summary>
    [TestClass]
    public class AdminSecurityTests {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials() {
            // arrange
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            var model = new LoginViewModel {
                UserName = "admin",
                Password = "secret"
            };
            var target = new AccountController(mock.Object);

            // act
            var result = target.Login(model, "/MyUrl");

            // assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }


        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials() {
            // arrange
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            var model = new LoginViewModel {
                UserName = "bad",
                Password = "worse"
            };
            var target = new AccountController(mock.Object);

            // act
            var result = target.Login(model, "/MyUrl");

            // assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
