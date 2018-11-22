using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Models.Api;
using Northwind.Services.Interfaces;
using System.Linq;
using Northwind.Entities;

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

        [HttpGet]
        [Route("")]
        public ActionResult<ProductModel> GetPaged([FromQuery] QueryObject queryObject) {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var productsPaged = this._productService
                .GetAll()
                .OrderBy(x => x.ProductName)
                .Skip((queryObject.PageNumber.Value - 1) * queryObject.PageSize.Value)
                .Take(queryObject.PageSize.Value);

            return _mapper.Map<ProductModel>(productsPaged.ToList());
        }

        [HttpGet]
        [Route("{productId}")]
        public ActionResult<ProductModel> Get(int? productId) {
            if (!productId.HasValue){
                ModelState.AddModelError(string.Empty, "ProductId is required");
                return BadRequest(ModelState);
            }

            var product = this._productService
                .GetAll()
                .FirstOrDefault(x => x.ProductId.Equals(productId));

            if (product.Equals(null)) {
                ModelState.AddModelError(string.Empty, $"Product not found with id={productId}");
                return NotFound(ModelState);
            }

            return _mapper.Map<ProductModel>(product);
        }

        [HttpPut]
        [Route("{productId}")]
        public ActionResult<ProductModel> Update(int? productId, [FromBody] ProductModel updatedProduct) {
            if (!productId.HasValue){
                ModelState.AddModelError(string.Empty, "ProductId is required");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var exists = this._productService.GetAll().Any(x => x.ProductId.Equals(productId));
            if (!exists) {
                ModelState.AddModelError(string.Empty, $"Product not found with id={productId.Value}");
                return NotFound(ModelState);
            }
            
            _productService.Update(_mapper.Map<Product>(updatedProduct));

            return updatedProduct;
        }

        [HttpPost]
        [Route("{productId}")]
        public ActionResult<int> Create([FromBody] CreateProductModel newProduct) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var product = _mapper.Map<Product>(newProduct);
            _productService.Add(product);

            return product.ProductId;
        }

        [HttpDelete]
        [Route("{productId}")]
        public ActionResult Create(int? productId) {
            if (!productId.HasValue){
                ModelState.AddModelError(string.Empty, "ProductId is required");
                return BadRequest(ModelState);
            }

            var product = this._productService.GetAll().FirstOrDefault(x => x.ProductId.Equals(productId));
            if (product.Equals(null)) {
                ModelState.AddModelError(string.Empty, $"Product not found with id={productId.Value}");
                return NotFound(ModelState);
            }
            
            _productService.Remove(product);

            return Ok();
        }
    }
}