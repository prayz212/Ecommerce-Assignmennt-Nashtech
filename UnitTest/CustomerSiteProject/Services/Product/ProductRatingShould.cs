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

//This unit test is failed because researching how to mock user identity value in HttpContextAccessor
namespace UnitTest.CustomerSiteProject.Services.Product
{
    public class ProductRatingShould
    {
        [Fact]
        public async Task ReturnOkWhenResponseIsSuccess()
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

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var productService = new ProductService(mockHttpClientFactory.Object, mockHttpContextAccessor.Object);

            //Act
            var result = await productService.ProductRating(new ProductRatingWriteDto { ProductId = 1, Stars = 5 });

            //Assert
            Assert.Equal(CustomerSite.Utils.PostResponse.OK, result);
        }

        [Fact]
        public async Task ReturnBadRequestWhenResponseIsNotSuccess()
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

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var productService = new ProductService(mockHttpClientFactory.Object, mockHttpContextAccessor.Object);

            //Act
            var result = await productService.ProductRating(new ProductRatingWriteDto { ProductId = 1, Stars = 5 });

            //Assert
            Assert.Equal((int)response.StatusCode, result);
        }
    }
}