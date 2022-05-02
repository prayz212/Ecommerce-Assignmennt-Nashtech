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
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenUpdateSuccess()
        {
            //Arrange
            var mockData = new CategoryDetailDto();
            var mockReturnData = new CategoryDetailDto
            {
                Id = 1,
                Name = "Dummy name",
                DisplayName = "Dummy display name",
                Description = "Dummy description"
            };

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.UpdateCategory(mockData)).ReturnsAsync(mockReturnData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.UpdateCategory(mockData);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(mockReturnData, objectResult.Value);
        }
    }
}