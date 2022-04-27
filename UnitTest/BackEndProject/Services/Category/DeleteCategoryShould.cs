using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
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
            BackEnd.Models.Category mockData = null;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

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
            var mockData = new BackEnd.Models.Category
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description",
                IsDeleted = false
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(mockData);
            mockCategoryRepository.Setup(r => r.UpdateCategory(mockData)).ReturnsAsync(false);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

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
            var mockData = new BackEnd.Models.Category
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description",
                IsDeleted = false
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(mockData);
            mockCategoryRepository.Setup(r => r.UpdateCategory(mockData)).ReturnsAsync(true);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.DeleteCategory(id);

            //Assert
            Assert.True(result);
        }
    }
}