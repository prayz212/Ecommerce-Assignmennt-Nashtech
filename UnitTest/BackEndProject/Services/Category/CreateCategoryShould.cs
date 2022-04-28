using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Admin;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class CreateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenSaveResultIsFalse()
        {
            //Arrange
            var category = new BackEnd.Models.Category
            {
                Name = "name",
                DisplayName = "display name",
                Description = "description"                
            };

            var createCategory = new CreateCategoryDto
            {
                name = "name",
                displayName = "display name",
                description = "description"
            };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.NewCategory(category)).ReturnsAsync(false);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            //Act
            var result = await categoryService.CreateCategory(createCategory);

            //Assert
            Assert.Null(result);
        }
    }
}