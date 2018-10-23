using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Entities;
using Northwind.Web.Models;
using Northwind.Services.Interfaces;
using System;
using System.IO;

namespace Northwind.Web.Pages.Categories {
    public class EditCategoryModel : PageModel {
        private IProductService _productService { get; set; }
        private ICategoryService _categoryService { get; set; }
        private ISupplierService _supplierService { get; set; }
        private IMapper _mapper { get; set; }

        [BindProperty]
        public EditCategoryViewModel CategoryModel { get; set; }

        [BindProperty]
        public string ImageLink { get; set; }

        public EditCategoryModel (
            ICategoryService categoryService,
            IMapper mapper
        ) {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult OnGet (int id) {
            if (id.Equals (-1) || id.Equals (0)) {
                return RedirectToCategoryList();
            }

            CategoryModel = _mapper.Map<EditCategoryViewModel>(_categoryService.GetAll().FirstOrDefault(x => x.CategoryId.Equals(id)));

            if (CategoryModel == null) {
                return RedirectToCategoryList();
            }
            ImageLink = $"https://{HttpContext.Request.Host}/Categories/images/{CategoryModel.CategoryId}";
            return Page ();
        }

        public IActionResult OnPost () {
            if (ModelState.IsValid) {
                var category = _categoryService.GetAll().FirstOrDefault(x => x.CategoryId.Equals(CategoryModel.CategoryId));

                category.CategoryName = CategoryModel.CategoryName;
                category.Description = CategoryModel.Description;
                if(CategoryModel.Image != null && CategoryModel.Image.Length > 0){
                    byte[] fileBytes;
                    byte[] randomBytes;
                    byte[] pictureBytes;
                    using (var stream = new MemoryStream())
                    {
                        CategoryModel.Image.CopyTo(stream);
                        fileBytes = stream.ToArray();
                    }
                    randomBytes = GenerateBufferFromSeed(78);
                    pictureBytes = new byte[randomBytes.Length + fileBytes.Length];
                    Buffer.BlockCopy(randomBytes, 0, pictureBytes, 0, randomBytes.Length);
                    Buffer.BlockCopy(fileBytes, 0, pictureBytes, randomBytes.Length, fileBytes.Length);
                    category.Picture = pictureBytes;
                }
                
                _categoryService.Update(category);
                _categoryService.SaveChanges ();
                return RedirectToAction ("Index", "Categories");
            }

            return Page();
        }

        private IActionResult RedirectToCategoryList () {
            return RedirectToAction ("Index", "Categories");
        }

        public byte[] GenerateBufferFromSeed(int size)
        {
            var _random = new Random();

            byte[] buffer = new byte[size];

            _random.NextBytes(buffer);

            return buffer;
        }
    }
}