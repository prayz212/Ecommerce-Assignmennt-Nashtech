using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.AdminSite.CategoryControllers
{
    public class GetAllCategoriesShould
    {
        [Theory]
        [InlineData(0, 5)]
        [InlineData(-1, 5)]
        [InlineData(1, 0)]
        [InlineData(1, -2)]
        public async Task ReturnBadRequestWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategories(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnCategoriesWhenSuccessful()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.AdminGetCategories(page, size)).ReturnsAsync(MockData.DummyCategoryListDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategories(page, size);
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyCategoryListDto, objectResult.Value);
        }

        [Fact]
        public async Task ReturnCategoriesWhenUsingDefaultParams()
        {
            //Arrange            
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(s => s.AdminGetCategories(1, 10)).ReturnsAsync(MockData.DummyCategoryListDto);

            var categoryController = new AdminCategoryController(mockCategoryService.Object);

            //Act
            var result = await categoryController.GetAllCategories();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(MockData.DummyCategoryListDto, objectResult.Value);
        }
    }
}