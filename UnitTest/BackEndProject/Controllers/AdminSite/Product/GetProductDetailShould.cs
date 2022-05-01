using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            ProductDetailDto product = null;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProductDetail(id)).ReturnsAsync(product);

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
            var product = new ProductDetailDto
            {
                id = 1,
                name = "product 1",
                description = "description 1",
                prices = 120000,
                averageRate = 4.5,
                isFeatured = false,
                category = "category 1",
                images = null,
                createdAt = null,
                updatedAt = null
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProductDetail(id)).ReturnsAsync(product);

            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetProductDetail(id);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(product, objectResult.Value);
        }
    }
}