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
    public class RelativeProductsViewComponentShould
    {
        [Fact]
        public async Task ReturnViewComponentWhenInvoke()
        {
            //Arrage
            IEnumerable<ProductReadDto> mockData = null;
            var id = 1;
            var size = 4;

            var mockSharedService = new Mock<IProductService>();
            mockSharedService.Setup(s => s.GetRelativeProductData(id, size)).ReturnsAsync(mockData);

            var relativeProductsViewComponent = new RelativeProductsViewComponent(mockSharedService.Object);

            //Act
            var result = await relativeProductsViewComponent.InvokeAsync(id, size);

            //Assert
            Assert.IsType<ViewViewComponentResult>(result);
        }
    }
}