using System;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class GetCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenNotHavingCategory()
        {
            //Arrange
            int id = new Random().Next();
            BackEnd.Models.Category mockData = null;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.GetCategory(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnDataWhenHavingCategory()
        {
            //Arrange
            int id = new Random().Next();
            var mockData = new BackEnd.Models.Category
            {
                Id = id,
                Name = "name",
                DisplayName = "display name",
                Description = "description",
                IsDeleted = false
            };

            var expectedValue = new CategoryDetailDto
            {
                id = id,
                name = "name",
                displayName = "display name",
                description = "description",
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.GetCategory(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedValue.id, result.id);
            Assert.Equal(expectedValue.name, result.name);
            Assert.Equal(expectedValue.displayName, result.displayName);
            Assert.Equal(expectedValue.description, result.description);
        }
    }
}