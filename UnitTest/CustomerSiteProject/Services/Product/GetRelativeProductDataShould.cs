using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CustomerSite.Services;
using CustomerSite.Utils;
using Moq;
using Moq.Protected;
using Shared.Clients;
using Xunit;

namespace UnitTest.CustomerSiteProject.Services.Product
{
    public class GetRelativeProductDataShould
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
                    thumbnailUri = "https://res.cloudinary.com/dazdxrnam/image/upload/v1643018065/NongSanBaoLam/DSCF7556_1643018065.jpg"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""name"": ""San pham 1"", ""prices"": 120000, ""averageRate"": 5, ""thumbnailName"": ""Tao my 1"", ""thumbnailUri"": ""https://res.cloudinary.com/dazdxrnam/image/upload/v1643018065/NongSanBaoLam/DSCF7556_1643018065.jpg"" }]"),
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
            httpClient.BaseAddress = new System.Uri("https://localhost:4546/");

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(c => c.CreateClient(Constant.CLIENT_NAME)).Returns(httpClient);

            var productService = new ProductService(mockHttpClientFactory.Object);

            //Act
            var result = await productService.GetRelativeProductData(1, 9);

            //Assert
            Assert.Equal(expectedValues.Count, result.Count());
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
            httpClient.BaseAddress = new System.Uri("https://localhost:4546/");

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(c => c.CreateClient(Constant.CLIENT_NAME)).Returns(httpClient);

            var productService = new ProductService(mockHttpClientFactory.Object);

            //Act
            var result = await productService.GetRelativeProductData(1, 9);

            //Assert
            Assert.Null(result);
        }
    }
}