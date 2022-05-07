using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.CategoryServices
{
    public class DeleteCategoryShould
    {
        [Theory]
        [InlineData(99)]
        [InlineData(98)]
        public async Task ReturnFalseWhenCategoryIsNull(int id)
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(id)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.DeleteCategory(id);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalseWhenDeleteFail()
        {
            //Arrange
            int id = 1;
            IEnumerable<Product> products = MockData.DummyListProduct;
            var productIds = products.Select(p => p.Id);

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(id)).ReturnsAsync(MockData.DummyCategory);
            mockRepository.Setup(r => r.Products.GetAll(p => p.CategoryId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListProduct);
            mockRepository.Setup(r => r.Images.GetAll(i => productIds.Contains(i.ProductId), 0, 0, null, "")).ReturnsAsync(MockData.DummyListImage);
            mockRepository.Setup(r => r.Ratings.GetAll(r => productIds.Contains(r.ProductID), 0, 0, null, "")).ReturnsAsync(MockData.DummyListRating);
            
            mockRepository.Setup(r => r.Categories.Delete(MockData.DummyCategory)).Returns(false);
            mockRepository.Setup(r => r.Products.DeleteRange(MockData.DummyListProduct)).Returns(true);
            mockRepository.Setup(r => r.Images.DeleteRange(MockData.DummyListImage)).Returns(true);
            mockRepository.Setup(r => r.Ratings.DeleteRange(MockData.DummyListRating)).Returns(false);

            var mockAutoMapper = new Mock<IMapper>();
            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.DeleteCategory(id);

            //Assert
            Assert.False(result);
        }
    }
}