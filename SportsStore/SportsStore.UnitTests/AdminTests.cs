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
    }
}
