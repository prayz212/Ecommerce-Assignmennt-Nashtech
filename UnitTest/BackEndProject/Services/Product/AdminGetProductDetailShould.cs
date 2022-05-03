using Newtonsoft.Json;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using BackEnd.Models.ViewModels;

namespace UnitTest.BackEndProject.Services.Product
{
    public class AdminGetProductDetailShould
    {
        [Fact]
        public async Task ReturnNullWhenProductIsNull()
        {
            //Arrange
            int id = 100;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(id)).ReturnsAsync(MockData.NullProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProductDetail(id);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnProductWhenProductIsNotNull()
        {
            //Arrange
            int id = 1;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(id)).ReturnsAsync(MockData.DummyProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<ProductDetailDto>(MockData.DummyProduct)).Returns(MockData.DummyProductDetailDto);

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProductDetail(id);

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(MockData.DummyProductDetailDto), JsonConvert.SerializeObject(actualResult));
        }
    }
}