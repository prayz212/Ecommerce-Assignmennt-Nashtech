using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Shared.Clients;

namespace CustomerSite.Services
{
  public class HomeService : IHomeService
  {
    private readonly IHttpClientFactory _clientFactory;

    public HomeService(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
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