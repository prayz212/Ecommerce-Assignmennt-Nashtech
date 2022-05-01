using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSite.Controllers;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Product
{
    public class IndexShould
    {
        [Fact]
        public async Task ReturnRedirectToHomeIndexWhenCategoryIsNull()
        {
            //Arrange
            String category = null;
            var expectedRedirectAction = "Index";
            var expectedRedirectController = "Home";

            var mockProductService = new Mock<IProductService>();
            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Index(category);
            var objectResult = result as RedirectToActionResult;

            //Assert
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
            Assert.Equal(expectedRedirectController, objectResult.ControllerName);
        }
        
        [Fact]
        public async Task ReturnRedirectToActionWithCategoryWhenDataIsNull()
        {
            //Arrange
            String category = "TraiCayDaLat";
            int page = 1;
            int size = 9;

            var expectedRedirectKey = "category";
            var expectedRedirectValue = "TatCaSanPham";
            var expectedRedirectAction = "Index";

            ProductListReadDto mockData = null;
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetCategoryProductData(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Index(category);
            var objectResult = result as RedirectToActionResult;

            //Assert
            Assert.True(objectResult.RouteValues.Keys.Contains(expectedRedirectKey));
            Assert.True(objectResult.RouteValues.Values.Contains(expectedRedirectValue));
            Assert.Equal(expectedRedirectAction, objectResult.ActionName);
        }

        [Fact]
        public async Task ReturnViewWhenEverythingSuccess()
        {
            //Arrange
            String category = "TraiCayDaLat";
            int page = 1;
            int size = 12;

            ProductListReadDto mockData = new ProductListReadDto()
            {
                products = new List<ProductReadDto>(),
                currentPage = 1,
                totalPage = 1
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetCategoryProductData(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Index(category, page);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}