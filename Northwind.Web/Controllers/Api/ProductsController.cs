using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Models.Api;
using Northwind.Services.Interfaces;
using System.Linq;
using Northwind.Entities;
using System.Collections.Generic;
using Northwind.Web.Helpers;

namespace Northwind.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(
            IProductService productService,
            IMapper mapper) 
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetPaged([FromQuery] QueryObject queryObject) {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var productsPaged = this._productService
                .GetAll()
                .OrderBy(x => x.ProductName)
                .Skip((queryObject.PageNumber.Value - 1) * queryObject.PageSize.Value)
                .Take(queryObject.PageSize.Value);

            return Ok(_mapper.Map<IEnumerable<ProductModel>>(productsPaged.ToList()));
        }

        [HttpGet("{productId}")]
        public ActionResult<ProductModel> Get(int? productId) {
            if (!productId.HasValue){
                ModelState.AddModelError("ProductId", "ProductId is required");
                return BadRequest(ModelState);
            }

            var product = this._productService
                .GetAll()
                .FirstOrDefault(x => x.ProductId.Equals(productId));

            if (product == null) {
                ModelState.AddModelError("ProductId", $"Product not found with id={productId}");
                return NotFound(ModelStateHelper.ToJson(ModelState));
            }

            return _mapper.Map<ProductModel>(product);
        }

        [HttpPut("{productId}")]
        public ActionResult<ProductModel> Update(int? productId, [FromBody] ProductModel updatedProduct) {
            if (!productId.HasValue){
                ModelState.AddModelError("ProductId", "ProductId is required");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var exists = this._productService.GetAll().Any(x => x.ProductId.Equals(productId));
            if (!exists) {
                ModelState.AddModelError("ProductId", $"Product not found with id={productId.Value}");
                return NotFound(ModelState);
            }
            
            _productService.Update(_mapper.Map<Product>(updatedProduct));
            _productService.SaveChanges();

            return updatedProduct;
        }

        [HttpPost("")]
        public ActionResult<int> Create([FromBody] CreateProductModel newProduct) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var product = _mapper.Map<Product>(newProduct);
            _productService.Add(product);
            _productService.SaveChanges();

            return product.ProductId;
        }

        [HttpDelete("{productId}")]
        public ActionResult Delete(int? productId) {

            if (!productId.HasValue){
                ModelState.AddModelError("ProductId", "ProductId is required");
                return BadRequest(ModelState);
            }

            var product = this._productService.GetAll().FirstOrDefault(x => x.ProductId.Equals(productId));
            if (product.Equals(null)) {
                ModelState.AddModelError("ProductId", $"Product not found with id={productId.Value}");
                return NotFound(ModelState);
            }
            
            _productService.Remove(product);
            _productService.SaveChanges();

            return Ok();
        }
    }
}