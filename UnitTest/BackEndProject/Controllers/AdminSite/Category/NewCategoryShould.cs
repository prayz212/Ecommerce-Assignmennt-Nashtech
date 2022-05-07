using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.AdminSite.CategoryControllers
{
    public class NewCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenCreateResultIsNull()
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(MockData.DummyCreateCategoryDto)).ReturnsAsync(MockData.NullCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(MockData.DummyCreateCategoryDto);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenCreateSuccess()
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.CreateCategory(MockData.DummyCreateCategoryDto)).ReturnsAsync(MockData.DummyCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.NewCategory(MockData.DummyCreateCategoryDto);
            var objectResult = result as CreatedAtActionResult;

            //Assert
            Assert.Equal(ConstantVariable.CREATED_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyCategoryDetailDto, objectResult.Value);
        }
    }
}