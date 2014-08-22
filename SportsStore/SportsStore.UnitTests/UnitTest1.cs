using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests {
    [TestClass]
    public class UnitTest1 {
        private Mock<IProductRepository> getRepMock() {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
            });

            return mock;
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model() {
            // arrange
            var mock = getRepMock();
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            // assert
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }


        [TestMethod]
        public void Can_Paginate() {
            // arrange
            var mock = getRepMock();
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            // assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        
        [TestMethod]
        public void Can_Generate_Page_Links() {
            // arrange
            HtmlHelper myHelper = null;
            var pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // act
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }


        [TestMethod]
        public void Can_Filter_Products() {
            // arrange
            var mock = getRepMock();
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            var result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            // assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        
        [TestMethod]
        public void Can_Create_Categories() {
            // arrange
            var mock = getRepMock();
            var controller = new NavController(mock.Object);

            // act
            var results = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Cat1");
            Assert.AreEqual(results[1], "Cat2");
            Assert.AreEqual(results[2], "Cat3");
        }


        [TestMethod]
        public void Indicates_Selected_Category() {
            // arrange
            var mock = getRepMock();
            var controller = new NavController(mock.Object);
            var categoryToSelect = "Cat3";

            // act
            var result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // assert
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}
