using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository) {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var model = new ProductsListViewModel {
                Products = repository.Products
                    .Where(prod => category == null || prod.Category == category)
                    .OrderBy(prod => prod.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = (category == null) 
                        ? repository.Products.Count()
                        : repository.Products.Where(prod => prod.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }


        public FileContentResult GetImage(int productId) {
            var prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod == null) {
                return null;
            } else {
                return File(prod.ImageData, prod.ImageMimeType);
            }
        }
    }
}