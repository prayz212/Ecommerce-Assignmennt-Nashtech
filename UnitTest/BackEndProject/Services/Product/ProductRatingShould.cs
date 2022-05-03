using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnFalseWhenProductIdInvalid()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(MockData.IncorrectDummyProductRating);

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
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalseWhenProductNotFound()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(MockData.CorrectDummyProductRating.ProductID)).ReturnsAsync(MockData.NullProduct);
            
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(MockData.CorrectDummyProductRating);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrueWhenCreatedRating()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(MockData.CorrectDummyProductRating.ProductID)).ReturnsAsync(MockData.DummyProduct);
            
            var mockRatingRepository = new Mock<IRatingRepository>();
            mockRatingRepository.Setup(r => r.CreateProductRating(MockData.CorrectDummyProductRating)).ReturnsAsync(true);
            
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(MockData.CorrectDummyProductRating);

            //Assert
            Assert.True(result);
        }
    }
}