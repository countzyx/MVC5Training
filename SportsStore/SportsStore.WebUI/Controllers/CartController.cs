using System.Linq;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repoParam, IOrderProcessor procParam) {
            this.repository = repoParam;
            this.orderProcessor = procParam;
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


        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            if (ModelState.IsValid) {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            } else {
                return View(shippingDetails);
            }
        }
    }
}