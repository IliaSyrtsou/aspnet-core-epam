using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.Entities;
using Northwind.Web.Models;
using Northwind.Services.Interfaces;

namespace Northwind.Web.Pages.Products {
    public class CreateProductModel : PageModel {
        private IProductService _productService { get; set; }
        private ICategoryService _categoryService { get; set; }
        private ISupplierService _supplierService { get; set; }
        private IMapper _mapper { get; set; }

        [BindProperty]
        public CreateProductViewModel Product { get; set; }

        [BindProperty]
        public ICollection<Category> Categories { get; set; }
        [BindProperty]
        public ICollection<Supplier> Suppliers { get; set; }

        public CreateProductModel (
            IProductService productService,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IMapper mapper
        ) {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public IActionResult OnGet () {
            Product = new CreateProductViewModel();
            Categories = _categoryService.GetAll().ToList();
            Suppliers = _supplierService.GetAll().ToList();

            return Page ();
        }

        public IActionResult OnPost () {
            if (ModelState.IsValid) {
                var product = _mapper.Map<Product>(Product);
                _productService.Add(product);
                _productService.SaveChanges ();
                return RedirectToAction ("Index", "Products");
            }
            Categories = _categoryService.GetAll().ToList();
            Suppliers = _supplierService.GetAll().ToList();
    
            return Page();
        }

        private IActionResult RedirectToProductList () {
            return RedirectToAction ("Index", "Products");
        }
    }
}