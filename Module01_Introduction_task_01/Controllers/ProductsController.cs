using Microsoft.AspNetCore.Mvc;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Controllers
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
            return View();
        }
    }
}