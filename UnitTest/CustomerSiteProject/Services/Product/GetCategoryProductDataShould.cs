using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using UnitTest.Utils;
using System.Net;
using CustomerSite.Services;
using Shared.Clients;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace UnitTest.CustomerSiteProject.Services.Product
{
    public class GetCategoryProductDataShould
    {
        [Fact]
        public async Task ReturnDataWhenResponseIsSuccess()
        {
            //Arrange
            var expectedValues = new List<ProductReadDto>()
            {
                new ProductReadDto
                {
                    Id = 1,
                    Name = "San pham 1",
                    Prices = 120000,
                    AverageRate = 5,
                    ThumbnailName = "Tao my 1",
                    ThumbnailUri = "Tao my 1 uri"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""products"": [{ ""id"": 1, ""name"": ""San pham 1"", ""prices"": 120000, ""averageRate"": 5, ""thumbnailName"": ""Tao my 1"", ""thumbnailUri"": ""Tao my 1 uri"" }], ""totalPage"": 1, ""currentPage"": 1}"),
            };

            var mockHttpClient = new Mock<HttpMessageHandler>();
            mockHttpClient
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpClient.Object);
            httpClient.BaseAddress = new System.Uri(ConstantVariable.BASE_URL);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(c => c.CreateClient(ConstantVariable.CLIENT_NAME)).Returns(httpClient);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var productService = new ProductService(mockHttpClientFactory.Object, mockHttpContextAccessor.Object);

            //Act
            ProductListReadDto result = await productService.GetCategoryProductData("category", 1, 9);

            //Assert
            Assert.Equal(expectedValues.Count, result.Products.Count());
            Assert.Equal(1, result.TotalPage);
            Assert.Equal(1, result.CurrentPage);
        }

        [Fact]
        public async Task ReturnNullWhenResponseIsNotSuccess()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            var mockHttpClient = new Mock<HttpMessageHandler>();
            mockHttpClient
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpClient.Object);
            httpClient.BaseAddress = new System.Uri(ConstantVariable.BASE_URL);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(c => c.CreateClient(ConstantVariable.CLIENT_NAME)).Returns(httpClient);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var productService = new ProductService(mockHttpClientFactory.Object, mockHttpContextAccessor.Object);

            //Act
            var result = await productService.GetCategoryProductData("category", 1, 9);

            //Assert
            Assert.Null(result);
        }
    }
}