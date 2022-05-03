using System;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetProductsByCategoryShould
    {
        [Theory]
        [InlineData(null, 1, 9)]
        [InlineData("category 1", 0, 9)]
        [InlineData("category 1", 1, -2)]
        public async Task ReturnNullWhenPassingInvalidParams(string category, int page, int size)
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetProductsByCategory(category, page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenHavingProducts()
        {
            //Arrange
            String category = "TraiCayNgoaiNhap";
            int page = 1;
            int size = 8;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.CountProductsByCategory(category)).ReturnsAsync(MockData.DummyListProductReadDto.Count);
            mockProductRepository.Setup(r => r.GetProductsByCategory(category, page, size)).ReturnsAsync(MockData.DummyListProductReadDto);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductsByCategory(category, page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.Products, result.Products);
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, result.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, result.TotalPage);
        }
    }
}