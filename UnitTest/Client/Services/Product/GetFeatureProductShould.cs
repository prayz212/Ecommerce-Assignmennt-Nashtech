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
    public class GetFeatureProductShould
    {
        [Fact]
        public async Task ReturnNullWhenHavingInvalidPage()
        {
            //Arrange
            ProductListReadDto expectedResult = null;

            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"},
                new ProductReadDto() { id = 2, name = "Product 2", prices = 140000, averageRate = 4.5, thumbnailName = "Image 2", thumbnailUri = "Uri image 2"}
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mock.Setup(r => r.CountFeatureProduct()).ReturnsAsync(10);

            var productService = new ProductService(mock.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProduct(0, 6);

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
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"},
                new ProductReadDto() { id = 2, name = "Product 2", prices = 140000, averageRate = 4.5, thumbnailName = "Image 2", thumbnailUri = "Uri image 2"}
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mock.Setup(r => r.CountFeatureProduct()).ReturnsAsync(10);

            var productService = new ProductService(mock.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProduct(1, -1);

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
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"},
                new ProductReadDto() { id = 2, name = "Product 2", prices = 140000, averageRate = 4.5, thumbnailName = "Image 2", thumbnailUri = "Uri image 2"}
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.GetFeatureProducts(1, 6)).ReturnsAsync(products);
            mock.Setup(r => r.CountFeatureProduct()).ReturnsAsync(-1);

            var productService = new ProductService(mock.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProduct(1, 8);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            IList<ProductReadDto> products = new List<ProductReadDto>()
            {
                new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 4.5, thumbnailName = "Image 1", thumbnailUri = "Uri image 1"},
                new ProductReadDto() { id = 2, name = "Product 2", prices = 140000, averageRate = 4.5, thumbnailName = "Image 2", thumbnailUri = "Uri image 2"}
            };

            ProductListReadDto expectedResult = new ProductListReadDto()
            {
                products = products,
                currentPage = 1,
                totalPage = 3
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(r => r.GetFeatureProducts(1, 8)).ReturnsAsync(products);
            mock.Setup(r => r.CountFeatureProduct()).ReturnsAsync(24);

            var productService = new ProductService(mock.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProduct(1, 8);

            //Assert
            Assert.Equal(expectedResult.currentPage, actualResult.currentPage);
            Assert.Equal(expectedResult.totalPage, actualResult.totalPage);
            Assert.Equal(expectedResult.products.Count, actualResult.products.Count);
        }
    }
}