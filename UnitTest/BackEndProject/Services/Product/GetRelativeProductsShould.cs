using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetRelativeProductsShould
    {
        [Fact]
        public async Task ReturnNullWhenParamsInvalid()
        {
            //Arrange
            var productId = 1;
            var size = -3;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenCurrentProductNotFound()
        {
            //Arrange
            var productId = -1;
            var size = 4;
            BackEnd.Models.Product currentProduct = null;
            
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductById(productId)).ReturnsAsync(currentProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var productId = 1;
            var size = 4;

            var currentProduct = new BackEnd.Models.Product()
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Prices = 120000,
                CategoryId = 1,
                Images = null,
                Ratings = null,
            };

            var expectedResult = new List<ProductReadDto>()
            {
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4, thumbnailName = "image 1", thumbnailUri = "uri 1" },
                new ProductReadDto() { id = 2, name = "Product 2", prices = 120000, averageRate = 4, thumbnailName = "image 2", thumbnailUri = "uri 2" },
                new ProductReadDto() { id = 3, name = "Product 3", prices = 120000, averageRate = 4, thumbnailName = "image 3", thumbnailUri = "uri 3" },
            };
            
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductById(productId)).ReturnsAsync(currentProduct);
            mockProductRepository.Setup(r => r.GetRelativeProduct(currentProduct.CategoryId, currentProduct.Id, size)).ReturnsAsync(expectedResult);

            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.GetRelativeProducts(productId, size);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}