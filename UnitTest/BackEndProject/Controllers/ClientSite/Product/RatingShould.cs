using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ClientSite.ProductControllers
{
    public class RatingShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenRatingResultIsFalse ()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(MockData.IncorrectDummyProductRating)).ReturnsAsync(false);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(MockData.IncorrectDummyProductRating);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithMessageWhenRatingResultIsTrue ()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductRating(MockData.CorrectDummyProductRating)).ReturnsAsync(true);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.Rating(MockData.CorrectDummyProductRating);
            var objectResult = result as OkResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
        }
    }
}