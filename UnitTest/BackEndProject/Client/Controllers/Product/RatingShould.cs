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
    public class RatingShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenRatingResultIsFalse ()
        {
            //Arrange
            ProductRatingWriteDto mockData = new ProductRatingWriteDto()
            {
                productID = -1,
                star = 4
            };
            var expectedStatusCode = 400;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(false);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithMessageWhenRatingResultIsTrue ()
        {
            //Arrange
            ProductRatingWriteDto mockData = new ProductRatingWriteDto()
            {
                productID = 1,
                star = 4
            };
            var expectedStatusCode = 200;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(true);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as OkResult;

            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        }
    }
}