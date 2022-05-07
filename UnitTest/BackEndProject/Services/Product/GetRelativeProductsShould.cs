using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ProductServices
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
            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetRelativeProducts(id, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenCurrentProductNotFound()
        {
            //Arrange
            var productId = 100;
            var size = 4;
            
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(productId)).ReturnsAsync(MockData.NullProduct);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Null(result);
        }
    }
}