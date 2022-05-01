using CustomerSite.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Clients;
using Microsoft.Extensions.Caching.Memory;
using System;
using CustomerSite.Utils;

namespace CustomerSite.Services
{
    public class SharedService : ISharedService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;
        private const double CACHING_TIME_IN_MINUTES = 60 * 10;

        public SharedService(IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _cache = cache;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategoryData()
        {
            if (!_cache.TryGetValue<IEnumerable<CategoryReadDto>>(ConstantVariable.CATEGORY_CACHE_KEY, out IEnumerable<CategoryReadDto> cachingValue))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(CACHING_TIME_IN_MINUTES);
                
                using (var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_CATEGORIES());
                    var response = await client.SendAsync(request);
                    var categories = Enumerable.Empty<CategoryReadDto>();

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStreamAsync();
                        categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryReadDto>>(data);
                        _cache.Set<IEnumerable<CategoryReadDto>>(ConstantVariable.CATEGORY_CACHE_KEY, categories, options);
                    }

                    return categories;
                }
            } 

            return cachingValue;
        }
    }
}
