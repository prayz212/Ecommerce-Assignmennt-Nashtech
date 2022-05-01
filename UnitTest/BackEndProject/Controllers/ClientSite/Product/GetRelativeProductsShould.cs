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
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 5, thumbnailName = "image 1", thumbnailUri = "uri 1" },
                new ProductReadDto() { id = 2, name = "Product 2", prices = 120000, averageRate = 5, thumbnailName = "image 2", thumbnailUri = "uri 2" },
                new ProductReadDto() { id = 3, name = "Product 3", prices = 120000, averageRate = 5, thumbnailName = "image 3", thumbnailUri = "uri 3" },
                new ProductReadDto() { id = 4, name = "Product 4", prices = 120000, averageRate = 5, thumbnailName = "image 4", thumbnailUri = "uri 4" },
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
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 5, thumbnailName = "image 1", thumbnailUri = "uri 1" },
                new ProductReadDto() { id = 3, name = "Product 3", prices = 120000, averageRate = 5, thumbnailName = "image 3", thumbnailUri = "uri 3" },
                new ProductReadDto() { id = 4, name = "Product 4", prices = 120000, averageRate = 5, thumbnailName = "image 4", thumbnailUri = "uri 4" },
                new ProductReadDto() { id = 7, name = "Product 7", prices = 120000, averageRate = 5, thumbnailName = "image 7", thumbnailUri = "uri 7" },
                new ProductReadDto() { id = 9, name = "Product 9", prices = 120000, averageRate = 5, thumbnailName = "image 9", thumbnailUri = "uri 9" },
                new ProductReadDto() { id = 12, name = "Product 12", prices = 120000, averageRate = 5, thumbnailName = "image 12", thumbnailUri = "uri 12" },
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