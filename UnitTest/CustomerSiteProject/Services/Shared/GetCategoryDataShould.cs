using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CustomerSite.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.CustomerSiteProject.Services.Shared
{
    public class GetCategoryDataShould
    {
        [Fact]
        public async Task ReturnEmptyCategoriesWhenResponseIsNotSuccess()
        {
            //Arrange
            object cachingValue = null;
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachingValue)).Returns(false);
            
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

            var sharedService = new SharedService(mockHttpClientFactory.Object, mockMemoryCache.Object);

            //Act
            var result = await sharedService.GetCategoryData();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnCategoriesWhenResponseSuccess()
        {
            //Arrange
            object cachingValue = null;
            List<CategoryReadDto> categories = new List<CategoryReadDto>()
            {
                new CategoryReadDto() { Id = 1, Name = "TraiCayQuyNhon", DisplayName = "Trai Cay Quy Nhon", Description = "Day la trai cay quy nhon"}
            };
            var options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(60*10);

            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachingValue)).Returns(false);
            mockMemoryCache.Setup(c => c.Set(ConstantVariable.CATEGORY_CACHE_KEY, categories, options)).Returns(categories);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""name"": ""TraiCayQuyNhon"", ""displayName"": ""Trai Cay Quy Nhon"", ""description"": ""Day la trai cay quy nhon""}]"),
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

            var sharedService = new SharedService(mockHttpClientFactory.Object, mockMemoryCache.Object);

            //Act
            var result = await sharedService.GetCategoryData();

            //Assert
            Assert.Equal(result, categories);
        }

        [Fact]
        public async Task ReturnCategoriesWhenCached()
        {
            //Arrange
            object cachingValue = null;
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachingValue)).Returns(true);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var sharedService = new SharedService(mockHttpClientFactory.Object, mockMemoryCache.Object);

            //Act
            var result = await sharedService.GetCategoryData();

            //Assert
            Assert.Null(result);
        }
    }
}