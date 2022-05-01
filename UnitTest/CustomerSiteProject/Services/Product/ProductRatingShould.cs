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

namespace UnitTest.CustomerSiteProject.Services.Product
{
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnTrueWhenResponseIsSuccess()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
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
            var result = await productService.ProductRating(new ProductRatingWriteDto { productID = 1, star = 5 });

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ReturnFalseWhenResponseIsNotSuccess()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
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
            var result = await productService.ProductRating(new ProductRatingWriteDto { productID = 1, star = 5 });

            //Assert
            Assert.False(result);
        }
    }
}