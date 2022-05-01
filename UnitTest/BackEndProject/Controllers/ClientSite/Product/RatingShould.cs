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

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(false);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
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

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(true);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as OkResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
        }
    }
}