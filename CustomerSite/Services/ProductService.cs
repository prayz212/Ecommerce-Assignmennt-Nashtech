using CustomerSite.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared.Clients;
using System.Text;
using CustomerSite.Utils;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System;

namespace CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size)
        {
            using (var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_PRODUCTS_BY_CATEGORY(category, page, size));
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    return await data.DeserializeToCamelCaseAsync<ProductListReadDto>();
                }

                return null;
            }
        }

        public async Task<ProductDetailReadDto> GetProductDetailData(int id)
        {
            using (var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_PRODUCT_BY_ID(id));
                var response = await client.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var productDetail = await response.Content.ReadAsStreamAsync();
                    return await productDetail.DeserializeToCamelCaseAsync<ProductDetailReadDto>();
                }

                return null;
            }
        }

        public async Task<ProductListReadDto> GetFeaturedProductData(int page, int size)
        {
            using(var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_FEATURE_PRODUCTS(page, size));
                var response = await client.SendAsync(request);
                ProductListReadDto products = null;

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    products = await data.DeserializeToCamelCaseAsync<ProductListReadDto>();
                }
                
                return products;
            }
        }

        public async Task<IEnumerable<ProductReadDto>> GetRelativeProductData(int id, int size)
        {
            using(var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_RELATIVE_PRODUCTS(id, size));
                var response = await client.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    return await data.DeserializeToCamelCaseAsync<IEnumerable<ProductReadDto>>();
                }

                return null;
            }
        }

        public async Task<int> ProductRating(ProductRatingWriteDto data)
        {
            var token = _httpContextAccessor.HttpContext.User.Identity.Name;
            var expiration = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Expiration)).Value;
            
            try
            {
                var expirationDateTime = DateTime.Parse(expiration);
                var a = expirationDateTime.Subtract(DateTime.Now).Seconds > 0;
                if (expirationDateTime.Subtract(DateTime.Now).Seconds > 0)
                {
                    using (var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(UrlRequest.GET_URL_RATING_PRODUCT(), content);
                        return response.IsSuccessStatusCode 
                            ? PostResponse.OK
                            : (int)response.StatusCode;
                    }
                }
                else
                {
                    return PostResponse.UNAUTHORIZED;
                }
            }
            catch
            {
                //Parse error
                return PostResponse.INTERNAL_SERVER_ERROR;
            }
        }
    }
}
