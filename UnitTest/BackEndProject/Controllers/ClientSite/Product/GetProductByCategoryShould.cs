using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.ClientSite.Product
{
    public class GetProductByCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenProductsIsNull()
        {
            //Arrange
            var category = "TatCaSanPham";
            var page = 1;
            var size = 9;

            ProductListReadDto mockData = null;

            var expectedStatusCode = 400;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductByCategory(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductByCategory(category);
            var objectResult = result as BadRequestResult;
            
            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenProductsIsNotNNull()
        {
            //Arrange
            var category = "TatCaSanPham";
            var page = 1;
            var size = 9;

            ProductListReadDto mockData = new ProductListReadDto()
            {
                products = new List<ProductReadDto>() 
                {
                    new ProductReadDto() { id = 1, name = "Product 1", prices = 120000, averageRate = 5, thumbnailName = "image 1", thumbnailUri = "uri 1" },
                    new ProductReadDto() { id = 2, name = "Product 2", prices = 120000, averageRate = 5, thumbnailName = "image 2", thumbnailUri = "uri 2" },
                    new ProductReadDto() { id = 3, name = "Product 3", prices = 120000, averageRate = 5, thumbnailName = "image 3", thumbnailUri = "uri 3" },
                    new ProductReadDto() { id = 4, name = "Product 4", prices = 120000, averageRate = 5, thumbnailName = "image 4", thumbnailUri = "uri 4" },
                },
                totalPage = 1,
                currentPage = 1
            };

            var expectedStatusCode = 200;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductByCategory(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductByCategory(category, page, size);
            var objectResult = result as OkObjectResult;
            
            //Assert
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }
    }
}