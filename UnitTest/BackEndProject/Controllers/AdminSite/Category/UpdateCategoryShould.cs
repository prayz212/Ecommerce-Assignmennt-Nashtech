using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Admin;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Category
{
    public class UpdateCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenResultIsNull()
        {
            //Arrange
            var mockData = new CategoryDetailDto();
            CategoryDetailDto mockReturnData = null;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.UpdateCategory(mockData)).ReturnsAsync(mockReturnData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.UpdateCategory(mockData);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenUpdateSuccess()
        {
            //Arrange
            var mockData = new CategoryDetailDto();
            var mockReturnData = new CategoryDetailDto
            {
                id = 1,
                name = "Dummy name",
                displayName = "Dummy display name",
                description = "Dummy description"
            };

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.UpdateCategory(mockData)).ReturnsAsync(mockReturnData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.UpdateCategory(mockData);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockReturnData, objectResult.Value);
        }
    }
}