using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CustomerSite.Services;
using UnitTest.Utils;
using Moq;
using Moq.Protected;
using Shared.Clients;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace UnitTest.CustomerSiteProject.Services.Product
{
    public class GetProductDetailDataShould
    {
        [Fact]
        public async Task ReturnDataWhenResponseIsSuccess()
        {
            //Arrange
            var expectedValues = new ProductDetailReadDto
            {
                Id = 1,
                Name = "San pham 1",
                Prices = 120000,
                AverageRate = 5,
                Description = "Mo ta",
                Images = null
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 1, ""name"": ""San pham 1"", ""prices"": 120000, ""averageRate"": 5, ""description"": ""Mo ta"", ""images"": null }"),
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
            ProductDetailReadDto result = await productService.GetProductDetailData(1);

            //Assert
            Assert.Equal(expectedValues.Id, result.Id);
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
            var result = await productService.GetProductDetailData(-1);

            //Assert
            Assert.Null(result);
        }
    }
}