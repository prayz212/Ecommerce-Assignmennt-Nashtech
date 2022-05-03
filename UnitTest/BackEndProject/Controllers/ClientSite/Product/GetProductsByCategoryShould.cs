using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.ClientSite.Product
{
    public class GetProductsByCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenProductsIsNull()
        {
            //Arrange
            var category = "TatCaSanPham";
            var page = 1;
            var size = 9;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductsByCategory(category, page, size)).ReturnsAsync(MockData.NullProductListReadDto);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductsByCategory(category);
            var objectResult = result as BadRequestResult;
            
            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenProductsIsNotNNull()
        {
            //Arrange
            var category = "TatCaSanPham";
            var page = 1;
            var size = 9;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductsByCategory(category, page, size)).ReturnsAsync(MockData.DummyProductListReadDto);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductsByCategory(category, page, size);
            var objectResult = result as OkObjectResult;
            
            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductListReadDto, objectResult.Value);
        }
    }
}