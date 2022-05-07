using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using Shared.Clients;

namespace UnitTest.BackEndProject.ProductServices
{
    public class GetProductDetailByIdShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public async Task ReturnNullWhenProductIdInvalid(int id)
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductDetailById(id);
            
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var productId = 1;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(productId)).ReturnsAsync(MockData.DummyProduct);

            var mockAutoMapper = new Mock<IMapper>();            
            mockAutoMapper.Setup(m => m.Map<ProductDetailReadDto>(MockData.DummyProduct)).Returns(MockData.DummyProductDetailReadDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.Equal(MockData.DummyProductDetailReadDto, result);
        }
    }
}