using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.ClientSite.Product
{
    public class GetProductsByCategoryShould
    {
        [Fact]
        public async Task ReturnBadRequestWhenProductsIsNull()
        {
            //Arrange
            var category = "TatCaSanPham";
            var page = 1;
            var size = 9;

            ProductListReadDto mockData = null;

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductsByCategory(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductsByCategory(category);
            var objectResult = result as BadRequestResult;
            
            //Assert
            Assert.Equal(ConstantVariable.BAD_REQUEST_STATUS_CODE, objectResult.StatusCode);
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

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProductsByCategory(category, page, size)).ReturnsAsync(mockData);

            var productController = new ProductController(mockProductService.Object);

            //Act
            var result = await productController.GetProductsByCategory(category, page, size);
            var objectResult = result as OkObjectResult;
            
            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(mockData, objectResult.Value);
        }
    }
}