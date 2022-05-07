using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using BackEnd.Models.ViewModels;

namespace UnitTest.BackEndProject.ProductServices
{
    public class AdminGetProductDetailShould
    {
        [Fact]
        public async Task ReturnNullWhenProductIsNull()
        {
            //Arrange
            int id = 100;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(id)).ReturnsAsync(MockData.NullProduct);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(id)).ReturnsAsync(MockData.DummyProduct);
            
            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<ProductDetailDto>(MockData.DummyProduct)).Returns(MockData.DummyProductDetailDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProductDetail(id);

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(MockData.DummyProductDetailDto, actualResult);
        }
    }
}