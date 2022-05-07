using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.ProductServices
{
    public class GetAllProductsShould
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
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenHavingProducts()
        {
            //Arrange
            int page = 1;
            int size = 6;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(null)).ReturnsAsync(MockData.DummyListProduct.Count());
            mockRepository.Setup(r => r.Products.GetAll(null, page, size, null, "Images,Ratings,Category")).ReturnsAsync(MockData.DummyListProduct);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductReadDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.Products.Count() , result.Products.Count());
            Assert.Equal(MockData.DummyProductListReadDto.Products, result.Products);
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, result.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, result.TotalPage);
        }
    }
}