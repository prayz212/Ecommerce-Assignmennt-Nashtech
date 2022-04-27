using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Category
{
    public class DeleteCategoryShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task ReturnBadRequestWhenPassingInvalidParam(int id)
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.DeleteCategory(id);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenResultIsFalse()
        {
            //Arrange
            int id = 99;
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.DeleteCategory(id)).ReturnsAsync(false);
            
            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.DeleteCategory(id);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenResultIsTrue()
        {
            //Arrange
            int id = 1;
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.DeleteCategory(id)).ReturnsAsync(true);
            
            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.DeleteCategory(id);
            var objectResult = result as OkResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}