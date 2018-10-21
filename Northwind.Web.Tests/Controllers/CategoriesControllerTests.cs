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

namespace Northwind.Web.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void Index_GetAllCategories()
        {
            // Arrange
            var category1 = new Category() {
                CategoryName = "category1",
                CategoryId = 1
            };
            var category2 = new Category() {
                CategoryName = "category2",
                CategoryId = 2
            };
                
            var categories = new List<Category>() {category1, category2};
            var queryable = categories.AsQueryable();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetAll()).Returns(queryable);
            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var persons = viewResult.Model.Should().BeAssignableTo<IEnumerable<Category>>().Subject;

            persons.Count().Should().Be(categories.Count);
        }
    }
}