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
    public class GetFeatureProductsShould
    {
        [Fact]
        public async Task ReturnNullWhenHavingInvalidPage()
        {
            //Arrange
            ProductListReadDto expectedResult = null;

            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1"},
                new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 140000, AverageRate = 4.5, ThumbnailName = "Image 2", ThumbnailUri = "Uri image 2"}
            };

            var mockProductRepository = new Mock<IProductRepository>();            
            mockProductRepository.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(10);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(0, 6);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ReturnNullWhenHavingInvalidSize()
        {
            //Arrange
            ProductListReadDto expectedResult = null;

            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1"},
                new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 140000, AverageRate = 4.5, ThumbnailName = "Image 2", ThumbnailUri = "Uri image 2"}
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(10);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, -1);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ReturnNullWhenTotalPageEqualMinusOne()
        {
            //Arrange
            ProductListReadDto expectedResult = null;

            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1"},
                new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 140000, AverageRate = 4.5, ThumbnailName = "Image 2", ThumbnailUri = "Uri image 2"}
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(-1);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, 8);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 4.5, ThumbnailName = "Image 1", ThumbnailUri = "Uri image 1"},
                new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 140000, AverageRate = 4.5, ThumbnailName = "Image 2", ThumbnailUri = "Uri image 2"}
            };

            ProductListReadDto expectedResult = new ProductListReadDto()
            {
                Products = products,
                CurrentPage = 1,
                TotalPage = 3
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetFeatureProducts(1, 8)).ReturnsAsync(products);
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(24);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, 8);

            //Assert
            Assert.Equal(expectedResult.CurrentPage, actualResult.CurrentPage);
            Assert.Equal(expectedResult.TotalPage, actualResult.TotalPage);
            Assert.Equal(expectedResult.Products.Count, actualResult.Products.Count);
        }
    }
}