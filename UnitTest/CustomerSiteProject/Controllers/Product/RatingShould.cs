using System.Threading.Tasks;
using CustomerSite.Controllers;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Product
{
    public class RatingShould
    {
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(1, 0)]
        public async Task ReturnBadRequestWhenPassingInvalidPrams(int id, int star)
        {
            //Arrange
            var data = new ProductRatingWriteDto
            {
                ProductID = id,
                Stars = star
            };

            var mockProductService = new Mock<IProductService>();
            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(data);
            var objectResult = result as BadRequestResult;

            //Assert;
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenRatingSuccess()
        {
            //Arrange
            ProductRatingWriteDto mockData = new ProductRatingWriteDto
            {
                ProductID = 1,
                Stars = 5
            };

            var expectedValue = true;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(expectedValue);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as OkResult;

            //Assert;
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenRatingFail()
        {
            //Arrange
            ProductRatingWriteDto mockData = null;
            var expectedValue = false;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(mockData)).ReturnsAsync(expectedValue);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }
    }
}