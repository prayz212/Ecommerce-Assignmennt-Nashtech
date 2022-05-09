using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ProductServices
{
    public class DeleteProductShould
    {
        [Fact]
        public async Task ReturnFalseWhenProductIsNull()
        {
            //Arrange
            int id = 0;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(id)).ReturnsAsync(MockData.NullProduct);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.DeleteProduct(id);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ReturnFalseWhenDeleteResultIsFalse()
        {
            //Arrange
            int id = 1;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(id)).ReturnsAsync(MockData.DummyProduct);
            mockRepository.Setup(r => r.Images.GetAll(i => i.ProductId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListImage);
            mockRepository.Setup(r => r.Ratings.GetAll(r => r.ProductId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListRating);

            mockRepository.Setup(r => r.Products.Delete(MockData.DummyProduct)).Returns(false);
            mockRepository.Setup(r => r.Images.DeleteRange(MockData.DummyListImage)).Returns(true);
            mockRepository.Setup(r => r.Ratings.DeleteRange(MockData.DummyListRating)).Returns(true);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.DeleteProduct(id);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ReturnTrueWhenDeleteResultIs()
        {
            //Arrange
            int id = 1;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Products.GetById(id)).ReturnsAsync(MockData.DummyProduct);
            mockRepository.Setup(r => r.Images.GetAll(i => i.ProductId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListImage);
            mockRepository.Setup(r => r.Ratings.GetAll(r => r.ProductId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListRating);

            mockRepository.Setup(r => r.Products.Delete(MockData.DummyProduct)).Returns(true);
            mockRepository.Setup(r => r.Images.DeleteRange(MockData.DummyListImage)).Returns(true);
            mockRepository.Setup(r => r.Ratings.DeleteRange(MockData.DummyListRating)).Returns(true);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.DeleteProduct(id);

            //Assert
            Assert.True(actual);
        }
    }
}