using CustomerSite.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Clients;
using System.Text;

namespace CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string CLIENT_NAME = "API_SERVER";

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size)
        {
            using (var client = _clientFactory.CreateClient(CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"products/?category={category}&page={page}&size={size}");
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
            using (var client = _clientFactory.CreateClient(CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "products/" + id);
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
            using(var client = _clientFactory.CreateClient(CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"products/features?page={page}&size={size}");
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
            using(var client = _clientFactory.CreateClient(CLIENT_NAME))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"products/relative/{id}?size={size}");
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
            using(var client = _clientFactory.CreateClient(CLIENT_NAME))
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("products/rating", content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
