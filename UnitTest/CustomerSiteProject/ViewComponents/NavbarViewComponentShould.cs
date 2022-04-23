using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.ViewComponents
{
    public class NavbarViewComponentShould
    {
        [Fact]
        public async Task ReturnViewComponentWhenInvoke()
        {
            //Arrage
            IEnumerable<CategoryReadDto> mockData = null;

            var mockSharedService = new Mock<ISharedService>();
            mockSharedService.Setup(s => s.GetCategoryData()).ReturnsAsync(mockData);

            var navbarViewComponent = new NavbarViewComponent(mockSharedService.Object);

            //Act
            var result = await navbarViewComponent.InvokeAsync();

            //Assert
            Assert.IsType<ViewViewComponentResult>(result);
        }
    }
}