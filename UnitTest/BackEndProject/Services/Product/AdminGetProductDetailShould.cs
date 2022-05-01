using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class AdminGetProductDetailShould
    {
        [Fact]
        public async Task ReturnNullWhenProductIsNull()
        {
            //Arrange
            int id = 100;
            BackEnd.Models.Product product = null;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(id)).ReturnsAsync(product);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var actualResult = await productService.AdminGetProductDetail(id);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnProductWhenProductIsNotNull()
        {
            //Arrange
            int id = 1;
            var product = new BackEnd.Models.Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Category = new BackEnd.Models.Category { Id = 1, Name = "Category1", DisplayName = "Category 1", Description = "Description 1" },
                Images = new List<BackEnd.Models.Image>
                {
                    new BackEnd.Models.Image { Id = 1, Name = "Image 1", ProductId = 1, Uri = "Uri 1" }
                },
                IsFeatured = true,
                Prices = 120000,
                Ratings = new List<BackEnd.Models.Rating>
                {
                    new BackEnd.Models.Rating { Id = 1, ProductID = 1, Stars = 5 },
                },
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var expectedProduct = new ProductDetailDto
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                category = product.Category.DisplayName,
                prices = product.Prices,
                isFeatured = product.IsFeatured,
                averageRate = 5,
                images = new List<ImageReadDto>
                {
                    new ImageReadDto { name = "Image 1", uri = "Uri 1"},
                },
                createdAt = product.CreatedDate.ToString("dd/MM/yyyy"),
                updatedAt = product.UpdatedDate.ToString("dd/MM/yyyy"),
            };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(id)).ReturnsAsync(product);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object);

            //Act
            var actualResult = await productService.AdminGetProductDetail(id);

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(JsonConvert.SerializeObject(expectedProduct), JsonConvert.SerializeObject(actualResult));
        }
    }
}