using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Northwind.Web.Controllers;
using Northwind.Entities;
using Northwind.Services.Interfaces;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Northwind.Web.Models;

namespace Northwind.Web.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private IList<Product> products { get; set; }
        private IList<EditProductViewModel> productModels { get; set; }

        public ProductsControllerTests() {
            Initialize();
        }

        private void Initialize() {
            products = new List<Product>();
            productModels = new List<EditProductViewModel>();
            for(int i=1;i<20;i++) {
                products.Add(new Product() {
                    ProductName = $"Product{i}",
                    ProductId = i
                });
                productModels.Add(new EditProductViewModel() {
                    ProductName = $"Product{i}",
                    ProductId = i
                });
            }
        }

        [Fact]
        public void Index_GetAllProducts_ShouldReturn10Products()
        {
            // Arrange
            var queryable = products.AsQueryable();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetAll()).Returns(queryable);
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(
                    x => x.Map<IList<EditProductViewModel>>(It.IsAny<IList<Product>>()))
                .Returns(productModels.Take(10).AsQueryable().ToList());
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x=> x["Products:MaxCount"]).Returns("10");
            var controller = new ProductsController(productServiceMock.Object, configMock.Object, mapperMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var returnedProducts = viewResult.Model.Should().BeAssignableTo<IList<EditProductViewModel>>().Subject;

            returnedProducts.Count().Should().Be(10);
        }

        [Fact]
        public void Index_GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var queryable = products.AsQueryable();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetAll()).Returns(queryable);
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IList<EditProductViewModel>>(It.IsAny<IList<Product>>())).Returns(productModels);
            var configMock = new Mock<IConfiguration>();
            var controller = new ProductsController(productServiceMock.Object, configMock.Object, mapperMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var returnedProducts = viewResult.Model.Should().BeAssignableTo<IList<EditProductViewModel>>().Subject;

            returnedProducts.Count().Should().Be(productModels.Count);
        }
    }
}