using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;


namespace SportsStore.UnitTests {
    [TestClass]
    public class AdminTests {
        private Mock<IProductRepository> getProdMock() {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"}
            });

            return mock;
        }

        [TestMethod]
        public void Index_Contains_All_Products() {
            // arrange
            var mock = getProdMock();
            var target = new AdminController(mock.Object);

            // action
            var result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }


        [TestMethod]
        public void Can_Edit_Product() {
            // arrange
            var mock = getProdMock();
            var target = new AdminController(mock.Object);

            // act
            var p1 = target.Edit(1).ViewData.Model as Product;
            var p2 = target.Edit(2).ViewData.Model as Product;
            var p3 = target.Edit(3).ViewData.Model as Product;

            // assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }


        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product() {
            // arrange
            var mock = getProdMock();
            var target = new AdminController(mock.Object);

            // act
            var result = (Product)target.Edit(4).ViewData.Model;

            // assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Can_Save_Valid_Changes() {
            // arrange
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };

            // act
            ActionResult result = target.Edit(product);

            // assert
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public void Cannot_Save_Invalid_Changes() {
            // arrange
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            // act
            ActionResult result = target.Edit(product);

            // assert
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public void Can_Delete_Valid_Products() {
            // arrange
            var mock = getProdMock();
            var target = new AdminController(mock.Object);
            var prod = new Product { ProductID = 2, Name = "P2" };

            // act
            target.Delete(prod.ProductID);

            // assert
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
