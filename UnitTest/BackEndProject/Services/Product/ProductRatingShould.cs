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
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnFalseWhenProductIdInvalid()
        {
            //Arrange
            var data = new ProductRatingWriteDto()
            {
                ProductID = -1,
                Star = 5
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
                ProductID = 1,
                Star = 0
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
                ProductID = 1,
                Star = 4
            };

            ProductDetailReadDto productDetail = null;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProductDetailById(data.ProductID)).ReturnsAsync(productDetail);
            
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
                ProductID = 1,
                Star = 4
            };

            ProductDetailReadDto productDetail = new ProductDetailReadDto()
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
            mockProductRepository.Setup(r => r.GetProductDetailById(data.ProductID)).ReturnsAsync(productDetail);
            
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