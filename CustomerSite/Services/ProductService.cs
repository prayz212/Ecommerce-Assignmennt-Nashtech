using CustomerSite.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Clients;
using System.Text;
using CustomerSite.Utils;

namespace CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size)
        {
            using (var client = _clientFactory.CreateClient(Constant.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_PRODUCTS_BY_CATEGORY(category, page, size));
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ProductListReadDto>(data);
                }

                return null;
            }
        }

        public async Task<ProductDetailReadDto> GetProductDetailData(int id)
        {
            using (var client = _clientFactory.CreateClient(Constant.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_PRODUCT_BY_ID(id));
                var response = await client.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var productDetail = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<ProductDetailReadDto>(productDetail);
                }

                return null;
            }
        }

        public async Task<ProductListReadDto> GetFeaturedProductData(int page, int size)
        {
            using(var client = _clientFactory.CreateClient(Constant.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_FEATURE_PRODUCTS(page, size));
                var response = await client.SendAsync(request);
                ProductListReadDto products = null;

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    products = await JsonSerializer.DeserializeAsync<ProductListReadDto>(data);
                }
                
                return products;
            }
        }

        public async Task<IEnumerable<ProductReadDto>> GetRelativeProductData(int id, int size)
        {
            using(var client = _clientFactory.CreateClient(Constant.CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_RELATIVE_PRODUCTS(id, size));
                var response = await client.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<IEnumerable<ProductReadDto>>(data);
                }

                return null;
            }
        }

        public async Task<bool> ProductRating(ProductRatingWriteDto data)
        {
            using(var client = _clientFactory.CreateClient(Constant.CLIENT_NAME))
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(UrlRequest.GET_URL_RATING_PRODUCT(), content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
