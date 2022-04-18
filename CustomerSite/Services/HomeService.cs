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
  }
}