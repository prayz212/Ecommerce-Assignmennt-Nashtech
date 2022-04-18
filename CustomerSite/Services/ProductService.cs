using CustomerSite.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Clients;

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
            using (var client = _clientFactory.CreateClient("API_SERVER"))
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
            using (var client = _clientFactory.CreateClient("API_SERVER"))
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
            using(var client = _clientFactory.CreateClient("API_SERVER"))
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
    }
}
