using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoriesController (
            ICategoryService categoryService,
            IMapper mapper) {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetPaged ([FromQuery] QueryObject queryObject) {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var categoriesPaged = this._categoryService
                .GetAll()
                .OrderBy(x => x.CategoryName)
                .Skip((queryObject.PageNumber.Value - 1) * queryObject.PageSize.Value)
                .Take(queryObject.PageSize.Value);

            return Ok(_mapper.Map<IEnumerable<CategoryModel>>(categoriesPaged.ToList()));
        }

        [HttpGet("{categoryId}/image")]
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

            return File (category.Picture.Skip(78).ToArray(), "image/*");
        }

        [HttpPut("{categoryId}/image")]
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

            var exists = this._categoryService
                .GetAll ()
                .Any (x => x.CategoryId.Equals (categoryId));

            if (!exists) {
                ModelState.AddModelError (string.Empty, $"Category not found with id={categoryId.Value}");
                return NotFound (ModelState);
            }

            var imageBytes = new byte[image.Length];
            using (var reader = new BinaryReader (image.OpenReadStream ())) {
                imageBytes = reader.ReadBytes(Convert.ToInt32(image.Length));
            }

            _categoryService.UpdateCategoryImage(categoryId.Value, imageBytes);
            _categoryService.SaveChanges();

            return Ok ();
        }
    }
}