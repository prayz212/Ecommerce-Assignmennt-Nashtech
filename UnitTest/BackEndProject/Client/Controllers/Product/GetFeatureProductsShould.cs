using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Client.Controllers.Product
{
    public class GetFeatureProductsShould
    {
        [Fact]
        public async Task ReturnOkWithValueWhenHavingData()
        {
            //Arrange
            var data = new ProductListReadDto()
            {
                products = new List<ProductReadDto>()
                {
                    new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"}
                },
                currentPage = 1,
                totalPage = 1
            };

            var expectedStatusCode = 200;
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProduct(1, 12)).ReturnsAsync(data);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 12);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenUsingDefaultValue()
        {
            //Arrange
            var data = new ProductListReadDto()
            {
                products = new List<ProductReadDto>(),
                currentPage = 2,
                totalPage = 5
            };

            var expectedStatusCode = 200;
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProduct(1, 6)).ReturnsAsync(data);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }

        [Fact]
        public async Task ReturnBadRequestWhenHavingNullResult()
        {
            //Arrange
            ProductListReadDto expectedValue = null;
            var expectedStatusCode = 400;
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProduct(1, 8)).ReturnsAsync(expectedValue);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 8);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        }
    }
}