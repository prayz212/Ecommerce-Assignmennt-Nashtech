using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;
using UnitTest.Utils;

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
                Name = "dummy name",
                DisplayName = "dummy display name",
                Description = "dummy description"
            };

            CategoryDetailDto expectedValue = null;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(mockData)).ReturnsAsync(expectedValue);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenCreateSuccess()
        {
            //Arrange
            var mockData = new CreateCategoryDto
            {
                Name = "dummy name",
                DisplayName = "dummy display name",
                Description = "dummy description"
            };

            var expectedValue = new CategoryDetailDto
            {
                Id = 1,
                Name = "dummy name",
                DisplayName = "dummy display name",
                Description = "dummy description"
            };

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(mockData)).ReturnsAsync(expectedValue);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(mockData);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(expectedValue, objectResult.Value);
        }
    }
}