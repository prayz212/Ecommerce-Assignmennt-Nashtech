using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.Controllers;
using Moq;
using Shared.Clients;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest.CustomerSiteProject.Controllers.Product
{
    public class FeaturedShould
    {
        [Fact]
        public async Task ReturnRedirectWhenDataIsNull()
        {
            //Arrange
            ProductListReadDto mockData = null;
            var expectedRedirectValue = 1;
            var expectedRedirectKey = "page";
            var expectedRedirectAction = "Featured";

            var page = 1;
            var size = 12;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Featured(page);
            var objectResult = result as RedirectToActionResult;

            //Assert
            Assert.True(objectResult.RouteValues.Keys.Contains(expectedRedirectKey));
            Assert.True(objectResult.RouteValues.Values.Contains(expectedRedirectValue));
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
        }

        [Fact]
        public async Task ReturnViewWhenPassingPageNumber()
        {
            //Arrange
            ProductListReadDto mockData = new ProductListReadDto();

            var page = 2;
            var size = 9;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Featured(page);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task ReturnViewWhenUsingDefautPageNumber()
        {
            //Arrange
            ProductListReadDto mockData = new ProductListReadDto();

            var page = 1;
            var size = 9;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Featured();

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}