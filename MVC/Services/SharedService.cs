using MVC.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels.Clients;

namespace MVC.Services
{
    public class SharedService : ISharedService
    {
        private readonly IHttpClientFactory _clientFactory;
        public SharedService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategoryData()
        {
            using (var client = _clientFactory.CreateClient("API_SERVER"))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "category");
                var response = await client.SendAsync(request);
                var categories = Enumerable.Empty<CategoryReadDto>();

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryReadDto>>(data);
                }

                return categories;
            }
        }
    }
}
