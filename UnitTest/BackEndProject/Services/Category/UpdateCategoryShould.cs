using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class UpdateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenCategoryIsNull()
        {
            //Arrange
            var dto = new CategoryDetailDto
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description"
            };

            BackEnd.Models.Category mockData = null;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(dto.Id)).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.UpdateCategory(dto);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenUpdateResultIsFalse()
        {
            //Arrange
            var inputData = new CategoryDetailDto
            {
                Id = 1,
                Name = "edited name",
                DisplayName = "edited display name",
                Description = "edited description"
            };

            var mockData = new BackEnd.Models.Category
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description",
                IsDeleted = false
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(inputData.Id)).ReturnsAsync(mockData);
            mockCategoryRepository.Setup(r => r.UpdateCategory(mockData)).ReturnsAsync(false);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.UpdateCategory(inputData);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnDataWhenUpdateResultIsTrue()
        {
            //Arrange
            var inputData = new CategoryDetailDto
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description"
            };

            var mockData = new BackEnd.Models.Category
            {
                Id = 1,
                Name = "name",
                DisplayName = "display name",
                Description = "description",
                IsDeleted = false
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(inputData.Id)).ReturnsAsync(mockData);
            mockCategoryRepository.Setup(r => r.UpdateCategory(mockData)).ReturnsAsync(true);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.UpdateCategory(inputData);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(inputData.Id, result.Id);
            Assert.Equal(inputData.Name, result.Name);
            Assert.Equal(inputData.DisplayName, result.DisplayName);
            Assert.Equal(inputData.Description, result.Description);
        }
    }
}