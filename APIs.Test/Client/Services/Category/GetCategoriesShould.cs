using APIs.Interfaces.Client;
using APIs.Services.Client;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Clients;
using Xunit;

namespace APIs.Test.Client.Services.Category
{
    public class GetCategoriesShould
    {
        [Fact]
        public async Task ReturnCategoriesWhenHavingData()
        {
            //Arrange
            IList<CategoryReadDto> categories = new List<CategoryReadDto>()
            {
                new CategoryReadDto()
                {
                    id = 1,
                    name = "TraiCayDaLat",
                    displayName = "Trai Cay Da Lat",
                    description = "Day la trai cay duoc trong o Da Lat"
                },
                new CategoryReadDto()
                {
                    id = 2,
                    name = "TraiCayQuyNhon",
                    displayName = "Trai Cay Quy Nhon",
                    description = "Day la trai cay duoc trong o Quy Nhon"
                }
            };

            var mock = new Mock<ICategoryRepository>();
            mock.Setup(c => c.GetCategories()).ReturnsAsync(categories);

            var categoryService = new CategoryService(mock.Object);
            //Act
            var actualResult = await categoryService.GetCategories();

            //Assert
            Assert.Equal(categories, actualResult);
        }
    }
}
