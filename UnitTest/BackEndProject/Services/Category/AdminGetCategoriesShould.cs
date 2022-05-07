using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.CategoryServices
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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.CountAll(null)).ReturnsAsync(count);

            var mockAutoMapper = new Mock<IMapper>();
            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

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

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.CountAll(null)).ReturnsAsync(MockData.DummyListCategory.Count);
            mockRepository.Setup(r => r.Categories.GetAll(null, page, size, null, "")).ReturnsAsync(MockData.DummyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(MockData.DummyListCategory)).Returns(MockData.DummyListCategoryDto);

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.AdminGetCategories(page, size);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(MockData.DummyListCategoryDto, result.Categories);
            Assert.Equal(MockData.DummyCategoryListDto.TotalPage, result.TotalPage);
            Assert.Equal(MockData.DummyCategoryListDto.CurrentPage, result.CurrentPage);
        }
    }
}