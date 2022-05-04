using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using Newtonsoft.Json;

namespace UnitTest.BackEndProject.Services.Category
{
    public class AdminGetCategoriesShould
    {
        [Theory]
        [InlineData(3, 5)]
        [InlineData(2, 10)]
        public async Task ReturnNullWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            int count = 10;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.CountAllCategories()).ReturnsAsync(count);

            var mockAutoMapper = new Mock<IMapper>();
            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.AdminGetCategories(page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnListDataWhenHavingCategories()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.CountAllCategories()).ReturnsAsync(MockData.DummyListCategory.Count);
            mockCategoryRepository.Setup(r => r.GetCategories(page, size)).ReturnsAsync(MockData.DummyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(MockData.DummyListCategory)).Returns(MockData.DummyListCategoryDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.AdminGetCategories(page, size);

            //Assert
            Assert.NotNull(result);
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyCategoryListDto), JsonConvert.SerializeObject(result)));
        }
    }
}