using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ProductServices
{
    public class GetFeatureProductsShould
    {
        [Theory]
        [InlineData(0, 6)]
        [InlineData(-1, 8)]
        [InlineData(1, 0)]
        [InlineData(0, -6)]
        public async Task ReturnNullWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(page, size);

            //Assert
            Assert.Null(actualResult);
        }

        [Theory]
        [InlineData(2,6)]
        [InlineData(3,4)]
        public async Task ReturnNullWhenTotalPageInvalid(int page, int size)
        {
            //Arrange
            int count = 6;
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(p => p.IsFeatured == true)).ReturnsAsync(count);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(page, size);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            int page = 1;
            int size = 8;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(p => p.IsFeatured == true)).ReturnsAsync(MockData.DummyListProduct.Count);
            mockRepository.Setup(r => r.Products.GetAll(p => p.IsFeatured == true, page, size, null, "Images,Ratings")).ReturnsAsync(MockData.DummyListProduct);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductReadDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, actualResult.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, actualResult.TotalPage);
            Assert.Equal(MockData.DummyProductListReadDto.Products, actualResult.Products);
        }
    }
}