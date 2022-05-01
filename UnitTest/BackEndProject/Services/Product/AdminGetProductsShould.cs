using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using BackEnd.Services;
using Moq;
using Xunit;

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
            mockProductRepository.Setup(r => r.CountAllProduct()).ReturnsAsync(count);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var actualResult = await productService.AdminGetProducts(page, size);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnProductListWhenHavingProducts()
        {
            //Arrange
            int count = 6;
            int page = 2;
            int size = 5;

            var products = new List<BackEnd.Models.Product>
            {
                new BackEnd.Models.Product 
                { 
                    Id = 1, 
                    Name = "Product 1", 
                    Prices = 120000,
                    Category = new BackEnd.Models.Category { Id = 1, Name = "Category1", DisplayName = "Category 1", Description = "Description" },
                    IsFeatured = true
                },
            };

            var expectedResult = new ProductListDto
            {
                products = new List<ProductDto>
                {
                    new ProductDto { id = 1, name = "Product 1", prices = 120000, category = "Category 1", isFeatured = true }
                },
                currentPage = page,
                totalPage = page,
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.CountAllProduct()).ReturnsAsync(count);
            mockProductRepository.Setup(r => r.GetProducts(page, size)).ReturnsAsync(products);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var actualResult = await productService.AdminGetProducts(page, size);

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(actualResult));
        }
    }
}