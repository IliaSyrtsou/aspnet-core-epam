using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Northwind.Services.Interfaces;
using Northwind.Web.Helpers;
using Northwind.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using Northwind.Entities;

namespace Northwind.Web.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
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
        
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public IActionResult Index() {
            var products = this._productService.GetAll();

            var count = _config["Products:MaxCount"].ToNullableInt();
            if (count != null && count != 0) {
                products = products.Take(count.Value);
            }

            var ordered = products.OrderBy(x=> x.ProductName);
            
            return View(_mapper.Map<IList<EditProductViewModel>>(ordered.ToList()));
        }
    }
}