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
                    id = 1,
                    name = "San pham 1",
                    prices = 120000,
                    averageRate = 5,
                    thumbnailName = "Tao my 1",
                    thumbnailUri = "Tao my 1 uri"
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

            var productService = new ProductService(mockHttpClientFactory.Object);

            //Act
            ProductListReadDto result = await productService.GetCategoryProductData("category", 1, 9);

            //Assert
            Assert.Equal(expectedValues.Count, result.products.Count);
            Assert.Equal(1, result.totalPage);
            Assert.Equal(1, result.currentPage);
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

            var productService = new ProductService(mockHttpClientFactory.Object);

            //Act
            var result = await productService.GetCategoryProductData("category", 1, 9);

            //Assert
            Assert.Null(result);
        }
    }
}