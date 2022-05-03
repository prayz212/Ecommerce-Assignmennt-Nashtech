using System;
using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Category
{
    public class GetCategoryDetailShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public async Task ReturnBadRequestWhenPassingInvalidId(int id)
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetCategoryDetail(id);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnNotfoundWhenNotHavingCategory()
        {
            //Arrange
            int id = 10;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.GetCategory(id)).ReturnsAsync(MockData.NullCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetCategoryDetail(id);
            var objectResult = result as NotFoundResult;

            //Assert
            Assert.Equal(ConstantVariable.NOT_FOUND_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenHavingCategory()
        {
            //Arrange
            int id = 1;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.GetCategory(id)).ReturnsAsync(MockData.DummyCategoryDetailDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetCategoryDetail(id);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyCategoryDetailDto, objectResult.Value);
        }
    }
}