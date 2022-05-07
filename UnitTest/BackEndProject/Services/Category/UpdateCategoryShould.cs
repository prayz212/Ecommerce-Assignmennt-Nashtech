using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using BackEnd.Models.ViewModels;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using BackEnd.Models;

namespace UnitTest.BackEndProject.CategoryServices
{
    public class UpdateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenUpdateResultIsFalse()
        {
            //Arrange
            CategoryDetailDto dto = MockData.DummyCategoryDetailDto;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.Update(MockData.DummyCategory)).ReturnsAsync(false);

            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.UpdateCategory(dto);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenUpdateResultIsTrue()
        {
            //Arrange
            CategoryDetailDto inputData = MockData.DummyCategoryDetailDto;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.Update(MockData.DummyCategory)).ReturnsAsync(true);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<Category>(MockData.DummyCategoryDetailDto)).Returns(MockData.DummyCategory);
            mockAutoMapper.Setup(m => m.Map<CategoryDetailDto>(MockData.DummyCategory)).Returns(MockData.DummyCategoryDetailDto);

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.UpdateCategory(inputData);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(MockData.DummyCategoryDetailDto, result);
        }
    }
}