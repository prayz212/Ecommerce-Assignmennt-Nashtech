using Newtonsoft.Json;
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
    public class GetAllProductShould
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
            var result = await productService.GetAllProduct(page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenHavingProducts()
        {
            //Arrange
            int page = 2;
            int size = 2;

            var rawProducts = new List<BackEnd.Models.Product>
            {
                new BackEnd.Models.Product { 
                    Id = 1, 
                    Name = "Product 1", 
                    Prices = 120000, 
                    Description = "mo ta", 
                    Ratings = new List<BackEnd.Models.Rating> 
                    { 
                        new BackEnd.Models.Rating { Id = 1, ProductID = 1, Stars = 5},
                        new BackEnd.Models.Rating { Id = 2, ProductID = 1, Stars = 4}
                    } ,
                    Images = new List<BackEnd.Models.Image>
                    {
                        new BackEnd.Models.Image { Id = 1, Name = "Image 1", ProductId = 1, Uri = "Uri image 1" },
                        new BackEnd.Models.Image { Id = 2, Name = "Image 2", ProductId = 1, Uri = "Uri image 2" },
                    }
                },
            };

            var expectedResult = new ProductListReadDto()
            {
                products = new List<ProductReadDto> {
                    new ProductReadDto() { 
                        id = 1, 
                        name = "Product 1", 
                        prices = 120000, 
                        averageRate = 4.5, 
                        thumbnailName = "Image 1", 
                        thumbnailUri = "Uri image 1"
                    },
                },
                currentPage = 2,
                totalPage = 2
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProducts(page, size)).ReturnsAsync(rawProducts);
            mockProductRepository.Setup(r => r.CountAllProduct()).ReturnsAsync(3);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetAllProduct(page, size);

            //Assert
            Assert.Equal(expectedResult.products.Count , result.products.Count);
            Assert.True(expectedResult.products.All(expected => result.products.Any(actual => 
            { 
                var objExpected = JsonConvert.SerializeObject(expected);
                var objActual = JsonConvert.SerializeObject(actual);

                return string.Equals(objExpected, objActual);
            })));
            Assert.Equal(expectedResult.currentPage, result.currentPage);
            Assert.Equal(expectedResult.totalPage, result.totalPage);
        }
    }
}