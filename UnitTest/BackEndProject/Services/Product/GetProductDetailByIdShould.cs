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
                id = 1,
                name = "Product 1",
                description = "Description 1",
                prices = 120000,
                averageRate = 4,
                images = new List<ImageReadDto>()
                {
                    new ImageReadDto() { name = "image 1", uri = "uri 1"},
                    new ImageReadDto() { name = "image 2", uri = "uri 2"},
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