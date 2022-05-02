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
    public class GetFeatureProductsShould
    {
        [Fact]
        public async Task ReturnOkWithValueWhenHavingData()
        {
            //Arrange
            var data = new ProductListReadDto()
            {
                Products = new List<ProductReadDto>()
                {
                    new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1"}
                },
                CurrentPage = 1,
                TotalPage = 1
            };
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 12)).ReturnsAsync(data);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 12);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenUsingDefaultValue()
        {
            //Arrange
            var data = new ProductListReadDto()
            {
                Products = new List<ProductReadDto>(),
                CurrentPage = 2,
                TotalPage = 5
            };
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 6)).ReturnsAsync(data);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }

        [Fact]
        public async Task ReturnBadRequestWhenHavingNullResult()
        {
            //Arrange
            ProductListReadDto expectedValue = null;
            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 8)).ReturnsAsync(expectedValue);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 8);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }
    }
}