using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.ViewComponents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.ViewComponents
{
    public class CategoriesViewComponentShould
    {
        [Theory]
        [InlineData("TatCaSanPham")]
        [InlineData("TraiCayDaLat")]
        public async Task ReturnViewComponentWhenInvoke(string category)
        {
            //Arrage
            IEnumerable<CategoryReadDto> mockData = null;
            var expectedValue = category;
            var expectedKey = "Current";

            var mockSharedService = new Mock<ISharedService>();
            mockSharedService.Setup(s => s.GetCategoryData()).ReturnsAsync(mockData);

            var categoriesViewComponent = new CategoriesViewComponent(mockSharedService.Object);

            //Act
            var result = await categoriesViewComponent.InvokeAsync(category);
            var viewResult = result as ViewViewComponentResult;

            //Assert
            Assert.IsType<ViewViewComponentResult>(result);
            Assert.True(viewResult.ViewData.Keys.Contains(expectedKey));
            Assert.True(viewResult.ViewData.Values.Contains(expectedValue));
        }
    }
}