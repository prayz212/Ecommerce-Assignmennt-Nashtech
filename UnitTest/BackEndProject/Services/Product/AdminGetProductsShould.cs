using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.ProductServices
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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(null)).ReturnsAsync(count);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(null)).ReturnsAsync(MockData.DummyListProduct.Count);
            mockRepository.Setup(r => r.Products.GetAll(null, page, size, null, "Category")).ReturnsAsync(MockData.DummyListProduct);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actualResult = await productService.AdminGetProducts(page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListDto.TotalPage, actualResult.TotalPage);
            Assert.Equal(MockData.DummyProductListDto.CurrentPage, actualResult.CurrentPage);
            Assert.Equal(MockData.DummyProductListDto.Products, actualResult.Products);
        }
    }
}