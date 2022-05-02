using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.ClientSite.Product
{
    public class GetRelativeProductsShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenProductIsNull()
        {
            //Arrange
            IEnumerable<ProductReadDto> mockData = null;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetRelativeProducts(100, 4)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetRelativeProducts(100, 4);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenUsingDefaultSize()
        {
            //Arrange
            var mockData = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 5, ThumbnailName = "image 1", ThumbnailUri = "uri 1" },
                new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 120000, AverageRate = 5, ThumbnailName = "image 2", ThumbnailUri = "uri 2" },
                new ProductReadDto() { Id = 3, Name = "Product 3", Prices = 120000, AverageRate = 5, ThumbnailName = "image 3", ThumbnailUri = "uri 3" },
                new ProductReadDto() { Id = 4, Name = "Product 4", Prices = 120000, AverageRate = 5, ThumbnailName = "image 4", ThumbnailUri = "uri 4" },
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetRelativeProducts(5, 4)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetRelativeProducts(5);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenPassAllParams()
        {
            //Arrange
            var mockData = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 5, ThumbnailName = "image 1", ThumbnailUri = "uri 1" },
                new ProductReadDto() { Id = 3, Name = "Product 3", Prices = 120000, AverageRate = 5, ThumbnailName = "image 3", ThumbnailUri = "uri 3" },
                new ProductReadDto() { Id = 4, Name = "Product 4", Prices = 120000, AverageRate = 5, ThumbnailName = "image 4", ThumbnailUri = "uri 4" },
                new ProductReadDto() { Id = 7, Name = "Product 7", Prices = 120000, AverageRate = 5, ThumbnailName = "image 7", ThumbnailUri = "uri 7" },
                new ProductReadDto() { Id = 9, Name = "Product 9", Prices = 120000, AverageRate = 5, ThumbnailName = "image 9", ThumbnailUri = "uri 9" },
                new ProductReadDto() { Id = 12, Name = "Product 12", Prices = 120000, AverageRate = 5, ThumbnailName = "image 12", ThumbnailUri = "uri 12" },
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetRelativeProducts(2, 6)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetRelativeProducts(2, 6);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }
    }
}