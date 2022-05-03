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
    public class UpdateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenCategoryIsNull()
        {
            //Arrange
            CategoryDetailDto dto = MockData.DummyCategoryDetailDto;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(dto.Id)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.UpdateCategory(dto);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnNullWhenUpdateResultIsFalse()
        {
            //Arrange
            CategoryDetailDto inputData = MockData.DummyCategoryDetailDto;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(inputData.Id)).ReturnsAsync(MockData.DummyCategory);
            mockCategoryRepository.Setup(r => r.UpdateCategory(MockData.DummyCategory)).ReturnsAsync(false);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.UpdateCategory(inputData);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnDataWhenUpdateResultIsTrue()
        {
            //Arrange
            CategoryDetailDto inputData = MockData.DummyCategoryDetailDto;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.GetCategory(inputData.Id)).ReturnsAsync(MockData.DummyCategory);
            mockCategoryRepository.Setup(r => r.UpdateCategory(MockData.DummyCategory)).ReturnsAsync(true);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<CategoryDetailDto>(MockData.DummyCategory)).Returns(MockData.DummyCategoryDetailDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.UpdateCategory(inputData);

            //Assert
            Assert.NotNull(result);
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyCategoryDetailDto), JsonConvert.SerializeObject(result)));
        }
    }
}