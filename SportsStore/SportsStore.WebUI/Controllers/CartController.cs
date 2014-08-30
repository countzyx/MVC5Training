using System.Linq;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller {
        private IProductRepository repository;

        public CartController(IProductRepository repoParam) {
            this.repository = repoParam;
        }


        public ViewResult Index(Cart cartParam, string returnUrlParam) {
            return View(new CartIndexViewModel {
                Cart = cartParam,
                ReturnUrl = returnUrlParam
            });
        }


        public RedirectToRouteResult AddToCart(Cart cartParam, int productId, string returnUrl) {
            var product = repository.Products
                .FirstOrDefault(prod => prod.ProductID == productId);

            if (product != null) {
                cartParam.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(Cart cartParam, int productId, string returnUrl) {
            var product = repository.Products
                .FirstOrDefault(prod => prod.ProductID == productId);

            if (product != null) {
                cartParam.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }


        public ViewResult Checkout() {
            return View(new ShippingDetails());
        }
    }
}