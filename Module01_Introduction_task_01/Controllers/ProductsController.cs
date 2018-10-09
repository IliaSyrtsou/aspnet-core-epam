using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Interfaces;

namespace Northwind.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController: Controller
    {
        private IProductService _productService { get; set; }

        public ProductsController(IProductService productService) {
            this._productService = productService;
        }
        public IActionResult Index()
        {
            var products = this._productService.GetAll().ToList();
            return View(products);
        }
    }
}