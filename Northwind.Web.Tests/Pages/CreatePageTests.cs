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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Tests.Controllers
{
    public class CreatePageTests
    {
        private IList<Category> _categories { get; set; }
        private IList<Supplier> _suppliers { get; set; }
        public CreatePageTests() {
            this.Initialize();
        }

        private void Initialize() {
            _categories = new List<Category>();
            _suppliers = new List<Supplier>();
            for(int i=0;i<20;i++) {
                _categories.Add(new Category() {
                    CategoryName = $"Category{i}",
                    CategoryId = i
                });
                _suppliers.Add(new Supplier() {
                    CompanyName = $"Company{i}",
                    SupplierId = i
                });
            }
        }

        [Fact]
        public void OnGet_ShouldReturnPageWithCategoriesAndSuppliers()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAll()).Returns(_categories.AsQueryable());
            var productServiceMock = new Mock<IProductService>();
            var supplierServiceMock = new Mock<ISupplierService>();
            supplierServiceMock.Setup(x => x.GetAll()).Returns(_suppliers.AsQueryable());
            var mapperMock = new Mock<IMapper>();
            var page = new CreateProductModel(
                productServiceMock.Object, 
                categoryServiceMock.Object, 
                supplierServiceMock.Object,
                mapperMock.Object);

            // Act
            var result = page.OnGet();

            // Assert
            var pageResult = result.Should().BeOfType<PageResult>().Subject;
            page.Categories.Count().Should().Be(_categories.Count);
            page.Suppliers.Count().Should().Be(_suppliers.Count);
            page.Product.Should().NotBeNull();
        }
    }
}