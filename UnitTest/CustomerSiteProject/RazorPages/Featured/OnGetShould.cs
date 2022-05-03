using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.RazorPages.Featured
{
    public class FeaturedShould
    {
        [Fact]
        public async Task ReturnDataWhenCalling()
        {
            //Arrange
            int page = 1;
            int size = 9;
            ProductListReadDto mockProductData = new ProductListReadDto
            {
                Products = new List<ProductReadDto>(),
                TotalPage = 1,
                CurrentPage = 1
            };
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetFeaturedProductData(page, size)).ReturnsAsync(mockProductData);

            List<CategoryReadDto> mockCategoryData = new List<CategoryReadDto>();
            var mockSharedService = new Mock<ISharedService>();
            mockSharedService.Setup(s => s.GetCategoryData()).ReturnsAsync(mockCategoryData);

            var featuredRazorPage = new CustomerSite.Pages.Product.Featured(mockProductService.Object, mockSharedService.Object);

            //Assert
            var result = await featuredRazorPage.OnGet();

            //Act
            Assert.IsType<PageResult>(result);
        }
    }
}