using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces.Client;
using BackEnd.Services.Client;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.Client.Services.Product
{
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnFalseWhenProductIdInvalid()
        {
            //Arrange
            var data = new ProductRatingWriteDto()
            {
                productID = -1,
                star = 5
            };

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalseWhenStarInvalid()
        {
            //Arrange
            var data = new ProductRatingWriteDto()
            {
                productID = 1,
                star = 0
            };

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalseWhenProductNotFound()
        {
            //Arrange
            var data = new ProductRatingWriteDto()
            {
                productID = 1,
                star = 4
            };

            ProductDetailReadDto productDetail = null;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductDetailById(data.productID)).ReturnsAsync(productDetail);
            
            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrueWhenCreatedRating()
        {
            //Arrange
            var data = new ProductRatingWriteDto()
            {
                productID = 1,
                star = 4
            };

            ProductDetailReadDto productDetail = new ProductDetailReadDto()
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
            mockProductRepository.Setup(r => r.GetProductDetailById(data.productID)).ReturnsAsync(productDetail);
            
            var mockRatingRepository = new Mock<IRatingRepository>();
            mockRatingRepository.Setup(r => r.CreateProductRating(data)).ReturnsAsync(true);

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.True(result);
        }
    }
}