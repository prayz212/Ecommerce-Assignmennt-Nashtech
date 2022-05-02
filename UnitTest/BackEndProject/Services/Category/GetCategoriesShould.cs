using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Services.Category
{
    public class GetCategoriesShould
    {
        [Fact]
        public async Task ReturnCategoriesWhenHavingData()
        {
            //Arrange
            IList<CategoryReadDto> expectedValue = new List<CategoryReadDto>()
            {
                new CategoryReadDto()
                {
                    Id = 1,
                    Name = "TraiCayDaLat",
                    DisplayName = "Trai Cay Da Lat",
                    Description = "Day la trai cay duoc trong o Da Lat"
                },
                new CategoryReadDto()
                {
                    Id = 2,
                    Name = "TraiCayQuyNhon",
                    DisplayName = "Trai Cay Quy Nhon",
                    Description = "Day la trai cay duoc trong o Quy Nhon"
                }
            };

            IList<BackEnd.Models.Category> mockData = new List<BackEnd.Models.Category>()
            {
                new BackEnd.Models.Category()
                {
                    Id = 1,
                    Name = "TraiCayDaLat",
                    DisplayName = "Trai Cay Da Lat",
                    Description = "Day la trai cay duoc trong o Da Lat",
                    IsDeleted = false
                },
                new BackEnd.Models.Category()
                {
                    Id = 2,
                    Name = "TraiCayQuyNhon",
                    DisplayName = "Trai Cay Quy Nhon",
                    Description = "Day la trai cay duoc trong o Quy Nhon",
                    IsDeleted = false
                }
            };

            var mock = new Mock<ICategoryRepository>();
            mock.Setup(c => c.GetCategories()).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mock.Object);
            
            //Act
            var actualResult = new List<CategoryReadDto>(await categoryService.GetCategories());

            //Assert
            Assert.NotEmpty(actualResult);
            Assert.Equal(expectedValue.Count, actualResult.Count);
        }

        [Fact]
        public async Task ReturnEmptyListWhenCategoriesIsEmpty()
        {
            //Arrange
            IList<CategoryReadDto> expectedValue = new List<CategoryReadDto>();
            IList<BackEnd.Models.Category> mockData = new List<BackEnd.Models.Category>();

            var mock = new Mock<ICategoryRepository>();
            mock.Setup(c => c.GetCategories()).ReturnsAsync(mockData);

            var categoryService = new CategoryService(mock.Object);
            
            //Act
            IList<CategoryReadDto> actualResult = new List<CategoryReadDto>(await categoryService.GetCategories());

            //Assert
            Assert.Empty(actualResult);
        }
    }
}
