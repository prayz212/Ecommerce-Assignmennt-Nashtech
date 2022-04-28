using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Category
{
    public class NewCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenCreateResultIsNull()
        {
            //Arrange
            var mockData = new CreateCategoryDto
            {
                name = "dummy name",
                displayName = "dummy display name",
                description = "dummy description"
            };

            CategoryDetailDto expectedValue = null;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(mockData)).ReturnsAsync(expectedValue);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenCreateSuccess()
        {
            //Arrange
            var mockData = new CreateCategoryDto
            {
                name = "dummy name",
                displayName = "dummy display name",
                description = "dummy description"
            };

            var expectedValue = new CategoryDetailDto
            {
                id = 1,
                name = "dummy name",
                displayName = "dummy display name",
                description = "dummy description"
            };

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(mockData)).ReturnsAsync(expectedValue);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(mockData);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(expectedValue, objectResult.Value);
        }
    }
}