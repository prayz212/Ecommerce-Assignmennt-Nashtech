using System.Collections.Generic;
using Newtonsoft.Json;
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
            var rawProduct = new BackEnd.Models.Product 
            { 
                Id = 1, 
                Name = "Product 1", 
                Prices = 120000, 
                Description = "Description 1", 
                Ratings = new List<BackEnd.Models.Rating> 
                {
                    new BackEnd.Models.Rating { Id = 2, ProductID = 1, Stars = 4}
                },
                Images = new List<BackEnd.Models.Image>
                {
                    new BackEnd.Models.Image { Id = 1, Name = "image 1", ProductId = 1, Uri = "uri 1" },
                    new BackEnd.Models.Image { Id = 2, Name = "image 2", ProductId = 1, Uri = "uri 2" },
                }
            };

            var expectedValue = new ProductDetailReadDto()
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
            mockProductRepository.Setup(r => r.GetProduct(productId)).ReturnsAsync(rawProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();
            
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.True(string.Equals(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(result)));
        }
    }
}