using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Product
{
    public class GetProductDetailShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public async Task ReturnBadRequestWhenPassingInvalidParam(int id)
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetProductDetail(id);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnNotFoundWhenProductIsNull()
        {
            //Arrange
            int id = 1;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProductDetail(id)).ReturnsAsync(MockData.NullProductDetailDto);

            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetProductDetail(id);
            var objectResult = result as NotFoundResult;

            //Assert
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenProductIsNotNull()
        {
            //Arrange
            int id = 1;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProductDetail(id)).ReturnsAsync(MockData.DummyProductDetailDto);

            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetProductDetail(id);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductDetailDto, objectResult.Value);
        }
    }
}