using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using Newtonsoft.Json;

namespace UnitTest.BackEndProject.Services.Category
{
    public class GetCategoriesShould
    {
        [Fact]
        public async Task ReturnCategoriesWhenHavingData()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(c => c.GetCategories()).ReturnsAsync(MockData.DummyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryReadDto>>(MockData.DummyListCategory)).Returns(MockData.DummyListCategoryReadDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = new List<CategoryReadDto>(await categoryService.GetCategories());

            //Assert
            Assert.NotEmpty(actualResult);
            Assert.Equal(MockData.DummyListCategoryReadDto.Count, actualResult.Count);
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyListCategoryReadDto), JsonConvert.SerializeObject(actualResult)));
        }

        [Fact]
        public async Task ReturnEmptyListWhenCategoriesIsEmpty()
        {
            //Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(c => c.GetCategories()).ReturnsAsync(MockData.EmptyListCategory);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<CategoryReadDto>>(MockData.EmptyListCategory)).Returns(MockData.EmptyListCategoryReadDto);

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockAutoMapper.Object);
            
            //Act
            IList<CategoryReadDto> actualResult = new List<CategoryReadDto>(await categoryService.GetCategories());

            //Assert
            Assert.Empty(actualResult);
        }
    }
}
