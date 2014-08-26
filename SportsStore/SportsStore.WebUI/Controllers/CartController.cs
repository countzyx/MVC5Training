﻿using System.Linq;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;


namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller {
        private IProductRepository repository;

        public CartController(IProductRepository repoParam) {
            this.repository = repoParam;
        }


        private Cart GetCart() {
            var cart = Session["Cart"] as Cart;
            if (cart == null) {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl) {
            var product = repository.Products
                .FirstOrDefault(prod => prod.ProductID == productId);

            if (product != null) {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl) {
            var product = repository.Products
                .FirstOrDefault(prod => prod.ProductID == productId);

            if (product != null) {
                GetCart().RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}