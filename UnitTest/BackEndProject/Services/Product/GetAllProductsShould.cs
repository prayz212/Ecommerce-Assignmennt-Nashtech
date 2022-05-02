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
    public class GetAllProductsShould
    {
        [Fact]
        public async Task ReturnNullWhenPassingInvalidParams()
        {
            //Arrange
            int page = 1;
            int size = 0;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenHavingProducts()
        {
            //Arrange
            int page = 2;
            int size = 2;

            var mockData = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1" },
            };

            var expectedResult = new ProductListReadDto()
            {
                Products = mockData,
                CurrentPage = 2,
                TotalPage = 2
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetAllProducts(page, size)).ReturnsAsync(mockData);
            mockProductRepository.Setup(r => r.CountAllProducts()).ReturnsAsync(3);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Equal(expectedResult.Products, result.Products);
            Assert.Equal(expectedResult.CurrentPage, result.CurrentPage);
            Assert.Equal(expectedResult.TotalPage, result.TotalPage);
        }
    }
}