using System;
using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Admin;
using Xunit;

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
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnNotfoundWhenNotHavingCategory()
        {
            //Arrange
            int id = 10;
            CategoryDetailDto mockData = null;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.GetCategory(id)).ReturnsAsync(mockData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetCategoryDetail(id);
            var objectResult = result as NotFoundResult;

            //Assert
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenHavingCategory()
        {
            //Arrange
            int id = 1;
            CategoryDetailDto mockData = new CategoryDetailDto
            {
                id = new Random().Next(),
                name = "Dummy name",
                displayName = "Dummy display name",
                description = "Dummy description"
            };

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.GetCategory(id)).ReturnsAsync(mockData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetCategoryDetail(id);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }
    }
}