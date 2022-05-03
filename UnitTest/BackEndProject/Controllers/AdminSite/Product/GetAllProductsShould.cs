using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Product
{
    public class GetAllProductsShould
    {
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(0, 5)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public async Task ReturnBadRequestWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenProductsIsNull()
        {
            //Arrange
            int page = 1;
            int size = 5;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProducts(page, size)).ReturnsAsync(MockData.NullProductListDto);
            
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenProductsIsNotNull()
        {
            //Arrange
            int page = 1;
            int size = 5;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProducts(page, size)).ReturnsAsync(MockData.DummyProductListDto);
            
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as OkObjectResult;

            //Assert
            var actualResult = objectResult.Value as ProductListDto;
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductListDto.Products, actualResult.Products);
            Assert.Equal(MockData.DummyProductListDto.TotalPage, actualResult.TotalPage);
            Assert.Equal(MockData.DummyProductListDto.CurrentPage, actualResult.CurrentPage);
        }
    }
}