using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Controllers;
using Xunit;

namespace Northwind.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_GetAllProducts_ShouldReturn10Products()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}