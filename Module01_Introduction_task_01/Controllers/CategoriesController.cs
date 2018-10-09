using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Controllers {
    [Route ("[controller]/[action]")]
    public class CategoriesController : Controller {
        private ICategoryService _categoriesService { get; set; }

        public CategoriesController(ICategoryService categoryService) {
            this._categoriesService = categoryService;
        }

        public IActionResult Index () {
            var categories = this._categoriesService.GetAll().ToList();
            return View(categories);
        }
    }
}