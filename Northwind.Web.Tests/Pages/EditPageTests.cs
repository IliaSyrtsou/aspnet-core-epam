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
using Northwind.Web.Pages.Products;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Models;
using Northwind.Web.Profiles;
using System;

namespace Northwind.Web.Tests.Controllers
{
    public class EditPageTests
    {
        private IList<Category> _categories { get; set; }
        private IList<Supplier> _suppliers { get; set; }
        private IList<Product> _products { get; set; }
        private IList<EditProductViewModel> _editProductViewModels { get; set; }
        public EditPageTests() {
            this.Initialize();
        }

        private void Initialize() {
            _categories = new List<Category>();
            _suppliers = new List<Supplier>();
            _products = new List<Product>();
            _editProductViewModels = new List<EditProductViewModel>();
            for(int i=1;i<20;i++) {
                _categories.Add(new Category() {
                    CategoryName = $"Category{i}",
                    CategoryId = i
                });
                _suppliers.Add(new Supplier() {
                    CompanyName = $"Company{i}",
                    SupplierId = i
                });
                _products.Add(new Product() {
                    ProductName = $"Product{i}",
                    ProductId = i
                });
                _editProductViewModels.Add(new EditProductViewModel() {
                    ProductName = $"Product{i}",
                    ProductId = i
                });
            }
        }

        [Fact]
        public void OnGet_ShouldReturnPageWithValidProductAndCategoriesAndSuppliers()
        {
            // Arrange
            int id = 1;
            var product = _products.FirstOrDefault(x => x.ProductId.Equals(id));
            var expectedProductModel = _editProductViewModels.FirstOrDefault(x => x.ProductId.Equals(id));
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAll()).Returns(_categories.AsQueryable());
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetAll()).Returns(_products.AsQueryable());
            var supplierServiceMock = new Mock<ISupplierService>();
            supplierServiceMock.Setup(x => x.GetAll()).Returns(_suppliers.AsQueryable());
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var page = new EditProductModel(
                productServiceMock.Object, 
                categoryServiceMock.Object, 
                supplierServiceMock.Object,
                mapper);

            // Act
            var result = page.OnGet(id);

            // Assert
            result.Should().BeOfType<PageResult>();
            page.Categories.Count().Should().Be(_categories.Count);
            page.Suppliers.Count().Should().Be(_suppliers.Count);
            page.Product.ProductId.Should().Be(expectedProductModel.ProductId);
        }

        [Fact]
        public void OnPost_ShouldReturnPageWithInvalidModelState()
        {
            // Arrange
            int id = 1;
            var productModel = _editProductViewModels.FirstOrDefault(x => x.ProductId.Equals(id));
            var productServiceMock = new Mock<IProductService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAll()).Returns(_categories.AsQueryable());
            var supplierServiceMock = new Mock<ISupplierService>();
            supplierServiceMock.Setup(x => x.GetAll()).Returns(_suppliers.AsQueryable());
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var page = new EditProductModel(
                productServiceMock.Object, 
                categoryServiceMock.Object, 
                supplierServiceMock.Object,
                mapper);
            page.Product = productModel;
            page.ModelState.AddModelError("UnitsInStock", "UnitsInStock is required");

            // Act
            var result = page.OnPost();

            // Assert
            result.Should().BeOfType<PageResult>();
            page.Categories.Count().Should().Be(_categories.Count);
            page.Suppliers.Count().Should().Be(_suppliers.Count);
        }
    }
}