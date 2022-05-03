using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetRelativeProductsShould
    {
        [Theory]
        [InlineData(0, 8)]
        [InlineData(-1, 0)]
        [InlineData(1, 0)]
        [InlineData(1, -5)]
        public async Task ReturnNullWhenParamsInvalid(int id, int size)
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetRelativeProducts(id, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenCurrentProductNotFound()
        {
            //Arrange
            var productId = -1;
            var size = 4;
            
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(productId)).ReturnsAsync(MockData.NullProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var productId = 1;
            var size = 4;
            
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(productId)).ReturnsAsync(MockData.DummyProduct);
            mockProductRepository.Setup(r => r.GetRelativeProducts(MockData.DummyProduct.CategoryId, MockData.DummyProduct.Id, size)).ReturnsAsync(MockData.DummyListProductReadDto);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Equal(MockData.DummyListProductReadDto, result);
        }
    }
}