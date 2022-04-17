using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<ProductReadDto>> GetFeaturedProductData()
    {
      using(var client = _clientFactory.CreateClient("API_SERVER"))
      {
        var request = new HttpRequestMessage(HttpMethod.Get, "products/features");
        var response = await client.SendAsync(request);
        var products = Enumerable.Empty<ProductReadDto>();

        if (response.IsSuccessStatusCode)
        {
          var data = await response.Content.ReadAsStreamAsync();
          products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductReadDto>>(data);
        }
        
        return products;
      }
    }
  }
}