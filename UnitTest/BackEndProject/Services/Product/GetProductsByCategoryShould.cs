using System;
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
    public class GetProductsByCategoryShould
    {
        [Theory]
        [InlineData(null, 1, 9)]
        [InlineData("category 1", 0, 9)]
        [InlineData("category 1", 1, -2)]
        public async Task ReturnNullWhenPassingInvalidParams(string category, int page, int size)
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.CountAll(p => p.Category.Name == category)).ReturnsAsync(MockData.DummyListProduct.Count);
            mockRepository.Setup(r => r.Products.GetAll(p => p.Category.Name == category, page, size, null, "Images,Ratings,Category")).ReturnsAsync(MockData.DummyListProduct);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductReadDto);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductsByCategory(category, page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.Products, result.Products);
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, result.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, result.TotalPage);
        }
    }
}