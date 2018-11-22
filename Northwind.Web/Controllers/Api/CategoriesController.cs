using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Interfaces;
using Northwind.Web.Models.Api;

namespace Northwind.Web.Controllers.Api {
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly ICategoryService _categoryService;
        public CategoriesController (
            ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route (":categoryId/image")]
        public ActionResult<CategoryModel> GetCategoryImage (int? categoryId) {
            if (!categoryId.HasValue) {
                ModelState.AddModelError (string.Empty, "CategoryId is required");
                return BadRequest (ModelState);
            }

            var category = this._categoryService
                .GetAll ()
                .FirstOrDefault (x => x.CategoryId.Equals (categoryId));

            if (category == null) {
                ModelState.AddModelError (string.Empty, $"Category not found with id={categoryId.Value}");
                return NotFound (ModelState);
            }

            return File (category.Picture, "image/*");
        }

        [HttpPut]
        [Route (":categoryId/image")]
        public ActionResult<CategoryModel> UpdateImageForCategory (int? categoryId, IFormFile image) {
            if (!categoryId.HasValue) {
                ModelState.AddModelError (string.Empty, "CategoryId is required");
                return BadRequest (ModelState);
            }

            if (image == null) {
                ModelState.AddModelError (string.Empty, "Image is required");
                return BadRequest (ModelState);
            }

            if (image.Length > Int32.MaxValue) {
                ModelState.AddModelError (string.Empty, $"Image is too large. Max size is {Int32.MaxValue} bytes");
                return BadRequest (ModelState);
            }

            var category = this._categoryService
                .GetAll ()
                .FirstOrDefault (x => x.CategoryId.Equals (categoryId));

            if (category == null) {
                ModelState.AddModelError (string.Empty, $"Category not found with id={categoryId.Value}");
                return NotFound (ModelState);
            }
            using (var reader = new BinaryReader (image.OpenReadStream ())) {
                category.Picture = reader.ReadBytes(Convert.ToInt32(image.Length));
            }

            _categoryService.Update (category);

            return Ok ();
        }
    }
}