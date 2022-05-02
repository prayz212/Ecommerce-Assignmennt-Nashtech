using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class AdminGetCategoriesShould
    {
        [Fact]
        public async Task ReturnEmptyListWhenNotHavingCategory()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategories(page, size)).ReturnsAsync(new List<BackEnd.Models.Category>());

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.AdminGetCategories(page, size);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnListDataWhenHavingCategories()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockData = new List<BackEnd.Models.Category>()
            {
                new BackEnd.Models.Category { Id = 1, Name = "category 1", DisplayName = "display name", Description = "description", IsDeleted = false},
                new BackEnd.Models.Category { Id = 2, Name = "category 2", DisplayName = "display name", Description = "description", IsDeleted = false},
                new BackEnd.Models.Category { Id = 3, Name = "category 3", DisplayName = "display name", Description = "description", IsDeleted = false},
            };

            var expectedValue = new List<CategoryDto>()
            {
                new CategoryDto { Id = 1, Name = "category 1", DisplayName = "display name" },
                new CategoryDto { Id = 2, Name = "category 2", DisplayName = "display name" },
                new CategoryDto { Id = 3, Name = "category 3", DisplayName = "display name" },
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategories(page, size)).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = new List<CategoryDto>(await categoryService.AdminGetCategories(page, size));

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expectedValue.Count, result.Count);
        }
    }
}