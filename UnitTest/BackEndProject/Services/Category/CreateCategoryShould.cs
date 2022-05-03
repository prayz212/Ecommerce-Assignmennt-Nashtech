using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.Services.Category
{
    public class CreateCategoryShould
    {
        [Fact]
        public async Task ReturnNullWhenSaveResultIsFalse()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(r => r.NewCategory(MockData.DummyCategory)).ReturnsAsync(false);
            
            var mockAutoMapper = new Mock<IMapper>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await categoryService.CreateCategory(MockData.DummyCreateCategoryDto);

            //Assert
            Assert.Null(result);
        }
    }
}