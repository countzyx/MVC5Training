using System.Linq;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller {
        private IProductRepository repository;


        public AdminController(IProductRepository repoParam) {
            repository = repoParam;
        }

        public ViewResult Index() {
            return View(repository.Products);
        }


        public ViewResult Edit(int productId) {
            var product = repository.Products.FirstOrDefault(prod => prod.ProductID == productId);
            return View(product);
        }
    }
}