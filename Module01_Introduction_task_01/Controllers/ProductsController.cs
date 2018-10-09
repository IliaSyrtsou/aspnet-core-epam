using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Northwind.Services.Interfaces;
using Northwind.Helpers;
using Northwind.Models;
using AutoMapper;
using System.Collections.Generic;
using Northwind.Entities;

namespace Northwind.Controllers {
    [Route ("[controller]")]
    public class ProductsController : Controller {
        private IProductService _productService { get; set; }
        private IConfiguration _config { get; set; }
        private IMapper _mapper { get; set; }
        
        
        public ProductsController(
            IProductService productService,
            IConfiguration config,
            IMapper mapper) 
        {
            this._productService = productService;
            this._config = config;
            this._mapper = mapper;
        }

        [Route("")]
        [Route("[action]")]
        public IActionResult Index() {
            var products = this._productService.GetAll();

            var count = _config["Products:MaxCount"].ToNullableInt();
            if (count != null && count != 0) {
                products = products.Take(count.Value);
            }

            return View(_mapper.Map<IList<ProductModel>>(products.ToList()));
        }

        [Route("[action]")]
        public IActionResult New() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(ProductModel model) {
            if (ModelState.IsValid){
                _productService.Add(_mapper.Map<Product>(model));
                _productService.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPut]
        public IActionResult Put(ProductModel model) {
            if (ModelState.IsValid){
                _productService.Add(_mapper.Map<Product>(model));
                _productService.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}