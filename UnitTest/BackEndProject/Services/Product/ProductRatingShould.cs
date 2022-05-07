using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ProductServices
{
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnFalseWhenProductIdInvalid()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

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
                Stars = 0
            };

            var mockRepository = new Mock<IUnitOfWork>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(data);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalseWhenProductNotFound()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(MockData.CorrectDummyProductRating.ProductID)).ReturnsAsync(MockData.NullProduct);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.ProductRating(MockData.CorrectDummyProductRating);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrueWhenCreatedRating()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(MockData.CorrectDummyProductRating.ProductID)).ReturnsAsync(MockData.DummyProduct);
            mockRepository.Setup(r => r.Ratings.Add(MockData.DummyRating)).ReturnsAsync(true);
            
            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<Rating>(MockData.CorrectDummyProductRating)).Returns(MockData.DummyRating);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.ProductRating(MockData.CorrectDummyProductRating);

            //Assert
            Assert.True(result);
        }
    }
}