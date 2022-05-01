using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Controllers.Admin;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.AdminSite.Product
{
    public class GetAllProductShould
    {
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(0, 5)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public async Task ReturnBadRequestWhenPassingInvalidParams(int page, int size)
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenProductsIsNull()
        {
            //Arrange
            int page = 1;
            int size = 5;
            ProductListDto products = null;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProducts(page, size)).ReturnsAsync(products);
            
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as BadRequestResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenProductsIsNotNull()
        {
            //Arrange
            int page = 1;
            int size = 5;
            var products = new ProductListDto
            {
                products = new List<ProductDto>
                {
                    new ProductDto { id = 1, name = "Product 1", category = "Category 1", isFeatured = true, prices = 120000 },
                },
                totalPage = 1,
                currentPage = 1,
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.AdminGetProducts(page, size)).ReturnsAsync(products);
            
            var adminProductController = new AdminProductController(mockProductService.Object);

            //Act
            var result = await adminProductController.GetAllProducts(page, size);
            var objectResult = result as OkObjectResult;

            //Assert
            var actualResult = objectResult.Value as ProductListDto;
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(products.products, actualResult.products);
            Assert.Equal(products.totalPage, actualResult.totalPage);
            Assert.Equal(products.currentPage, actualResult.currentPage);
        }
    }
}