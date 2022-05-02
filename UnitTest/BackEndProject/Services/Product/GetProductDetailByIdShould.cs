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
    public class GetProductDetailByIdShould
    {
        [Fact]
        public async Task ReturnNullWhenProductIdInvalid()
        {
            //Arrange
            var productId = -1;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var productId = 1;
            ProductDetailReadDto product = new ProductDetailReadDto()
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Prices = 120000,
                AverageRate = 4,
                Images = new List<ImageReadDto>()
                {
                    new ImageReadDto() { Name = "image 1", Uri = "uri 1"},
                    new ImageReadDto() { Name = "image 2", Uri = "uri 2"},
                }
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductDetailById(productId)).ReturnsAsync(product);

            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.Equal(product, result);
        }
    }
}