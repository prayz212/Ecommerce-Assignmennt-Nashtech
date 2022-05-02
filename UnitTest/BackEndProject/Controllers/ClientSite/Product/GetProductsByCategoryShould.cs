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
                Products = new List<ProductReadDto>() 
                {
                    new ProductReadDto() { Id = 1, Name = "Product 1", Prices = 120000, AverageRate = 5, ThumbnailName = "image 1", ThumbnailUri = "uri 1" },
                    new ProductReadDto() { Id = 2, Name = "Product 2", Prices = 120000, AverageRate = 5, ThumbnailName = "image 2", ThumbnailUri = "uri 2" },
                    new ProductReadDto() { Id = 3, Name = "Product 3", Prices = 120000, AverageRate = 5, ThumbnailName = "image 3", ThumbnailUri = "uri 3" },
                    new ProductReadDto() { Id = 4, Name = "Product 4", Prices = 120000, AverageRate = 5, ThumbnailName = "image 4", ThumbnailUri = "uri 4" },
                },
                TotalPage = 1,
                CurrentPage = 1
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