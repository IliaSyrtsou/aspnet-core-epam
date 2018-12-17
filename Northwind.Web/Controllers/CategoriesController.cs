using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Northwind.Web.Controllers {
    [Route ("[controller]/[action]")]
    public class CategoriesController : Controller {
        private ICategoryService _categoriesService { get; set; }

        public CategoriesController(ICategoryService categoryService) {
            this._categoriesService = categoryService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Authorize]
        public IActionResult Index () {
            var categories = this._categoriesService.GetAll().ToList();
            return View(categories);
        }

        [HttpGet]
        [Route("/images/{id}")]
        [Route("/[controller]/images/{id}")]
        public IActionResult GetImage(int? id) {
            if(id.HasValue) {
                var category = this._categoriesService.GetAll().FirstOrDefault(x => x.CategoryId.Equals(id.Value));
                if(category != null) {
                    var imageBytes = category.Picture.Skip(78).ToArray();
                    return new FileContentResult(imageBytes, "image/bmp");
                } else {
                    return BadRequest("No category found matching provided id.");
                }
            } else{
                return BadRequest("Invalid id.");
            }
        }
    }
}