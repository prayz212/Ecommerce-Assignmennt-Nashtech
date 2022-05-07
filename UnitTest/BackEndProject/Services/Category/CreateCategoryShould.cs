using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using BackEnd.Models;
using BackEnd.Models.ViewModels;

namespace UnitTest.BackEndProject.CategoryServices
{
    public class CreateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenSaveResultIsFalse()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.Add(MockData.DummyCategory)).ReturnsAsync(false);
            
            var mockAutoMapper = new Mock<IMapper>();
            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.CreateCategory(MockData.DummyCreateCategoryDto);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenSaveResultIsTrue()
        {
            //Arrange
            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<Category>(MockData.DummyCreateCategoryDto)).Returns(MockData.DummyCategory);
            mockAutoMapper.Setup(m => m.Map<CategoryDetailDto>(MockData.DummyCategory)).Returns(MockData.DummyCategoryDetailDto);

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.Add(MockData.DummyCategory)).ReturnsAsync(true);
            
            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.CreateCategory(MockData.DummyCreateCategoryDto);

            //Assert
            Assert.Equal(MockData.DummyCategoryDetailDto, result);
        }
    }
}