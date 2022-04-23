using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSite.Controllers;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Home
{
    public class IndexShould
    {
        [Fact]
        public async Task ReturnRedirectWhenDataIsNull()
        {
            //Arrange
            ProductListReadDto mockData = null;
            var expectedRedirectAction = "Index";
            var expectedRedirectController = "Error";
            var size = 12;
            var page = 1;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockData);

            var homeController = new HomeController(mockProductService.Object);

            //Act
            var result = await homeController.Index();
            var objectResult = result as RedirectToActionResult;
            
            //Assert
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
            Assert.Equal(expectedRedirectController, objectResult.ControllerName);
        }

        [Fact]
        public async Task ReturnViewWhenEverythingSuccess()
        {
            //Arrange
            ProductListReadDto mockData = new ProductListReadDto()
            {
                products = new List<ProductReadDto>() {},
                currentPage = 1,
                totalPage = 1
            };

            var size = 12;
            var page = 1;
            
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockData);

            var homeController = new HomeController(mockProductService.Object);

            //Act
            var result = await homeController.Index();
            
            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}