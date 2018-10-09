using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Northwind.Services.Interfaces;
using Northwind.Helpers;

namespace Northwind.Controllers {
    [Route ("[controller]/[action]")]
    public class ProductsController : Controller {
        private IProductService _productService { get; set; }
        private IConfiguration _config { get; set; }

        public ProductsController (
            IProductService productService,
            IConfiguration config
        ) {
            this._productService = productService;
            this._config = config;
        }
        public IActionResult Index() {
            var products = this._productService.GetAll();

            var count = _config["Products:MaxCount"].ToNullableInt();
            if (count != null && count != 0) {
                products = products.Take(count.Value);
            }

            return View(products.ToList());
        }
    }
}