using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.UpdateCategory(MockData.DummyCategoryDetailDto)).ReturnsAsync(MockData.NullCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.UpdateCategory(MockData.DummyCategoryDetailDto);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenUpdateSuccess()
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.UpdateCategory(MockData.DummyCategoryDetailDto)).ReturnsAsync(MockData.DummyCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.UpdateCategory(MockData.DummyCategoryDetailDto);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyCategoryDetailDto, objectResult.Value);
        }
    }
}