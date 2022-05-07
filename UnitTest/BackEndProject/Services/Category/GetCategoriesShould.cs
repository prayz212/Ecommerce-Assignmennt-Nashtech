using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.CategoryServices
{
    public class GetCategoriesShould
    {
        [Fact]
        public async Task ReturnCategoriesWhenHavingData()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(c => c.Categories.GetAll(null, 0, 0, null, "")).ReturnsAsync(MockData.DummyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryReadDto>>(MockData.DummyListCategory)).Returns(MockData.DummyListCategoryReadDto);

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await categoryService.GetCategories();

            //Assert
            Assert.NotEmpty(actualResult);
            Assert.Equal(MockData.DummyListCategoryReadDto, actualResult);
        }

        [Fact]
        public async Task ReturnEmptyListWhenCategoriesIsEmpty()
        {
            //Arrange
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(c => c.Categories.GetAll(null, 0, 0, null, "")).ReturnsAsync(MockData.EmptyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryReadDto>>(MockData.EmptyListCategory)).Returns(MockData.EmptyListCategoryReadDto);

            var categoryService = new CategoryService(mockRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await categoryService.GetCategories();

            //Assert
            Assert.Empty(actualResult);
        }
    }
}
