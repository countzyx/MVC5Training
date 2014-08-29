﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

using Moq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests {
    [TestClass]
    public class CartTests {

        private Product[] getProducts() {
            return new Product[] {
                new Product { ProductID = 1, Name = "P1", Price = 100M, Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Price = 50M },
                new Product { ProductID = 3, Name = "P3", Price = 25M }
            };
        }


        private Mock<IProductRepository> getMock() {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(getProducts().AsQueryable());
            return mock;
        }


        [TestMethod]
        public void Can_Add_New_Lines() {
            // arrange
            var products = getProducts();
            var target = new Cart();

            // act
            target.AddItem(products[0], 1);
            target.AddItem(products[1], 1);
            var results = target.Lines.ToArray();

            // assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, products[0]);
            Assert.AreEqual(results[1].Product, products[1]);
        }


        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines() {
            // arrange
            var products = getProducts();
            var target = new Cart();

            // act
            target.AddItem(products[0], 1);
            target.AddItem(products[1], 1);
            target.AddItem(products[0], 10);
            var results = target.Lines.OrderBy(line => line.Product.ProductID).ToArray();

            // assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }


        [TestMethod]
        public void Can_Remove_Line() {
            // arrange
            var products = getProducts();
            var target = new Cart();

            // act
            target.AddItem(products[0], 1);
            target.AddItem(products[1], 3);
            target.AddItem(products[2], 5);
            target.AddItem(products[1], 1);
            target.RemoveLine(products[1]);

            // assert
            Assert.AreEqual(target.Lines.Where(line => line.Product == products[1]).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }


        [TestMethod]
        public void Calculate_Cart_Total() {
            // arrange
            var products = getProducts();
            var target = new Cart();

            // act
            target.AddItem(products[0], 1);
            target.AddItem(products[1], 1);
            target.AddItem(products[0], 3);
            var result = target.ComputeTotalValue();

            // assert
            Assert.AreEqual(result, 450M);
        }


        [TestMethod]
        public void Can_Clear_Contents() {
            // arrange
            var products = getProducts();
            var target = new Cart();
            target.AddItem(products[0], 1);
            target.AddItem(products[1], 1);

            // act
            target.Clear();

            // assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }


        [TestMethod]
        public void Can_Add_To_Cart() {
            // arrange
            var mock = getMock();
            var cart = new Cart();
            var target = new CartController(mock.Object);

            // act
            target.AddToCart(cart, 1, null);

            // assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }


        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen() {
            // arrange
            var mock = getMock();
            var cart = new Cart();
            var target = new CartController(mock.Object);

            // act
            RedirectToRouteResult result = target.AddToCart(cart, 4, "myUrl");

            // assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }


        [TestMethod]
        public void Can_View_Cart_Contents() {
            // arrange
            var cart = new Cart();
            var target = new CartController(null);

            // act
            var result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
