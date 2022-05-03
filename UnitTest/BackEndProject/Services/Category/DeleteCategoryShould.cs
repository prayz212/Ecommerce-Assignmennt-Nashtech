using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class DeleteCategoryShould
    {
        [Theory]
        [InlineData(99)]
        [InlineData(98)]
        public async Task ReturnFalseWhenCategoryIsNull(int id)
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

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

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(MockData.DummyCategory);
            mockCategoryRepository.Setup(r => r.UpdateCategory(MockData.DummyCategory)).ReturnsAsync(false);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.DeleteCategory(id);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrueWhenDeleteSuccess()
        {
            //Arrange
            int id = 1;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(MockData.DummyCategory);
            mockCategoryRepository.Setup(r => r.UpdateCategory(MockData.DummyCategory)).ReturnsAsync(true);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.DeleteCategory(id);

            //Assert
            Assert.True(result);
        }
    }
}