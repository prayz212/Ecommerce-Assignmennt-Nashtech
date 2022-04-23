using System.Threading.Tasks;
using CustomerSite.Controllers;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Product
{
    public class DetailShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ReturnRedirectProductIndexWhenPassingInvalidParams(int productId)
        {
            //Arrange
            var expectedRedirectAction = "Index";
            var expectedRedirectKey = "category";
            var expectedRedirectValue = "TatCaSanPham";

            var mockProductService = new Mock<IProductService>();
            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Detail(productId);
            var objectResult = result as RedirectToActionResult;
            
            //Assert
            Assert.True(objectResult.RouteValues.Keys.Contains(expectedRedirectKey));
            Assert.True(objectResult.RouteValues.Values.Contains(expectedRedirectValue));
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
        }

        [Fact]
        public async Task ReturnRedirectWhenProductDetailIsNull()
        {
            //Arrange
            ProductDetailReadDto mockData = null;
            var expectedRedirectAction = "Index";
            var expectedRedirectController = "Error";
            var productId = 1;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailData(productId)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Detail(productId);
            var objectResult = result as RedirectToActionResult;
            
            //Assert
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
            Assert.Equal(expectedRedirectController, objectResult.ControllerName);
        }

        [Fact]
        public async Task ReturnViewWhenEverythingSuccess()
        {
            //Arrange
            ProductDetailReadDto mockData = new ProductDetailReadDto();
            var productId = 1;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailData(productId)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Detail(productId);
            
            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}