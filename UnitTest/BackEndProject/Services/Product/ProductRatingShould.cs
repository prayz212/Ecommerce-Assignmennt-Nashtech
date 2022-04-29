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

            BackEnd.Models.Product productDetail = null;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(data.productID)).ReturnsAsync(productDetail);
            
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

            var productDetail = new BackEnd.Models.Product()
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Prices = 120000,
                Ratings = null,
                Images = null
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(data.productID)).ReturnsAsync(productDetail);
            
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