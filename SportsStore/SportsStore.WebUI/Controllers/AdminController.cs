using System.Web.Mvc;

using SportsStore.Domain.Abstract;

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
    }
}