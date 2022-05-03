using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailById(1)).ReturnsAsync(MockData.DummyProductDetailReadDto);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductDetailById(1);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductDetailReadDto, objectResult.Value);
        }

        [Fact]
        public async Task  ReturnNotFoundWhenNotHavingProduct()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductDetailById(100)).ReturnsAsync(MockData.NullProductDetailReadDto);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductDetailById(100);
            var objectResult = result as NotFoundResult;

            //Assert
            Assert.Equal(ConstantVariable.NOT_FOUND_STATUS_CODE, objectResult.StatusCode);
        }
    }
}