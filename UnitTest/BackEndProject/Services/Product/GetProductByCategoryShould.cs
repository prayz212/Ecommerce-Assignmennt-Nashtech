using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetProductByCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenPassingInvalidParams()
        {
            //Arrange
            String category = null;
            int page = 1;
            int size = 9;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.GetProductByCategory(category, page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenHavingProducts()
        {
            //Arrange
            String category = "TraiCayNgoaiNhap";
            int page = 2;
            int size = 2;

            var mockData = new List<ProductReadDto>()
            {
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"},
            };

            var expectedResult = new ProductListReadDto()
            {
                products = mockData,
                currentPage = 2,
                totalPage = 2
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductByCategory(category, page, size)).ReturnsAsync(mockData);
            mockProductRepository.Setup(r => r.CountProductByCategory(category)).ReturnsAsync(3);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetProductByCategory(category, page, size);

            //Assert
            Assert.Equal(expectedResult.products, result.products);
            Assert.Equal(expectedResult.currentPage, result.currentPage);
            Assert.Equal(expectedResult.totalPage, result.totalPage);
        }
    }
}