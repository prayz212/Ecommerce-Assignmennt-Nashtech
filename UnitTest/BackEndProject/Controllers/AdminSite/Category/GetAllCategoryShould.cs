using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Admin;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Category
{
    public class GetAllCategoryShould
    {
        [Theory]
        [InlineData(0, 5)]
        [InlineData(-1, 5)]
        [InlineData(1, 0)]
        [InlineData(1, -2)]
        public async Task ReturnBadRequestWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            var mockData = new List<CategoryDto>();
            
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.AdminGetCategories(page, size)).ReturnsAsync(mockData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategory(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public async Task ReturnCategoriesWhenSuccessful(int page, int size)
        {
            //Arrange
            var mockData = new List<CategoryDto>();
            
            for(int item = 0; item < size; item++)
            {
                var dummyElement = new CategoryDto
                {
                    id = new Random().Next(),
                    name = "name" + item,
                    displayName = "display name " + item
                };

                mockData.Add(dummyElement);
            }
            
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.AdminGetCategories(page, size)).ReturnsAsync(mockData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategory(page, size);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }

        [Fact]
        public async Task ReturnCategoriesWhenUsingDefaultParams()
        {
            //Arrange
            var mockData = new List<CategoryDto>();
            
            for(int item = 0; item < 10; item++)
            {
                var dummyElement = new CategoryDto
                {
                    id = new Random().Next(),
                    name = "name" + item,
                    displayName = "display name " + item
                };

                mockData.Add(dummyElement);
            }
            
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.AdminGetCategories(1, 10)).ReturnsAsync(mockData);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategory();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }
    }
}