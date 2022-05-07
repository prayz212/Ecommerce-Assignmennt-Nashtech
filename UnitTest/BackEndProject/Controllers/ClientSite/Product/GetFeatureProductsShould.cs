using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ClientSite.ProductControllers
{
    public class GetFeatureProductsShould
    {
        [Fact]
        public async Task ReturnOkWithValueWhenHavingData()
        {
            //Arrange            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 12)).ReturnsAsync(MockData.DummyProductListReadDto);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 12);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductListReadDto, objectResult.Value);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenUsingDefaultValue()
        {
            //Arrange            
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 6)).ReturnsAsync(MockData.DummyProductListReadDto);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyProductListReadDto, objectResult.Value);
        }

        [Fact]
        public async Task ReturnBadRequestWhenHavingNullResult()
        {
            //Arrange
            var mock = new Mock<IProductService>();
            mock.Setup(s => s.GetFeatureProducts(1, 8)).ReturnsAsync(MockData.NullProductListReadDto);

            var productController = new ProductController(mock.Object);

            //Act
            var result = await productController.GetFeatureProducts(1, 8);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }
    }
}