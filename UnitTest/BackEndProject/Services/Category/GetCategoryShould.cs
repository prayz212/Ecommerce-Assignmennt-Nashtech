using System;
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
    public class GetCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenNotHavingCategory()
        {
            //Arrange
            int id = new Random().Next();

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.GetCategory(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnDataWhenHavingCategory()
        {
            //Arrange
            int id = new Random().Next();

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(id)).ReturnsAsync(MockData.DummyCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<CategoryDetailDto>(MockData.DummyCategory)).Returns(MockData.DummyCategoryDetailDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.GetCategory(id);

            //Assert
            Assert.NotNull(result);
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyCategoryDetailDto), JsonConvert.SerializeObject(result)));
        }
    }
}