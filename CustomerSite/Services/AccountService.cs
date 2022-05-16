using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.Utils;
using Shared.Clients;

namespace CustomerSite.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<TokenDto> Login(LoginDto dto)
        {
            using (var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(UrlRequest.GET_URL_LOGIN(), content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();
                    return await data.DeserializeToCamelCaseAsync<TokenDto>();
                }

                return null;
            }
        }

        public async Task<bool> Register(ClientRegisterDto dto)
        {
            using(var client = _clientFactory.CreateClient(ConstantVariable.CLIENT_NAME))
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(UrlRequest.GET_URL_REGISTER(), content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}