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
    public class GetProductDetailByIdShould
    {
        [Fact]
        public async Task ReturnOkWithValueWhenHavingData()
        {
            //Arrange
            var mockData = new ProductDetailReadDto()
            {
                id = 5,
                name = "Product 5",
                description = "This is description of product 5",
                averageRate = 4,
                prices = 120000,
                images = new List<ImageReadDto>()
                {
                    new ImageReadDto() { name = "image 1", uri = "uri 1" },
                    new ImageReadDto() { name = "image 2", uri = "uri 2" },
                }
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailById(5)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductDetailById(5);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }

        [Fact]
        public async Task  ReturnNotFoundWhenNotHavingProduct()
        {
            //Arrange
            ProductDetailReadDto mockData = null;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailById(100)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductDetailById(100);
            var objectResult = result as NotFoundResult;

            //Assert
            Assert.Equal(ConstantVariable.NOT_FOUND_STATUS_CODE, objectResult.StatusCode);
        }
    }
}