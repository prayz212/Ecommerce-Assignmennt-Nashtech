using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.Services.Product
{
    public class AdminGetProductsShould
    {
        [Theory]
        [InlineData(3, 5)]
        [InlineData(2, 10)]
        public async Task ReturnNullWhenPassingInvalidPage(int page, int size)
        {
            //Arrange
            int count = 10;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.CountAllProducts()).ReturnsAsync(count);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProducts(page, size);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnProductListWhenHavingProducts()
        {
            //Arrange
            int page = 1;
            int size = 6;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.CountAllProducts()).ReturnsAsync(MockData.DummyListProduct.Count);
            mockProductRepository.Setup(r => r.GetProducts(page, size)).ReturnsAsync(MockData.DummyListProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductDto);

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProducts(page, size);

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(MockData.DummyProductListDto), JsonConvert.SerializeObject(actualResult));
        }
    }
}