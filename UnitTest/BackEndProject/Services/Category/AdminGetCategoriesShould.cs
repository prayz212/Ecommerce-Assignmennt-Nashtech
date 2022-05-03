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
        [Fact]
        public async Task ReturnEmptyListWhenNotHavingCategory()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategories(page, size)).ReturnsAsync(MockData.EmptyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(MockData.EmptyListCategory)).Returns(MockData.EmptyListCategoryDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.AdminGetCategories(page, size);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnListDataWhenHavingCategories()
        {
            //Arrange
            int page = 1;
            int size = 10;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategories(page, size)).ReturnsAsync(MockData.DummyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(MockData.DummyListCategory)).Returns(MockData.DummyListCategoryDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = new List<CategoryDto>(await categoryService.AdminGetCategories(page, size));

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(MockData.DummyListCategoryDto.Count, result.Count);
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyListCategoryDto), JsonConvert.SerializeObject(result)));
        }
    }
}