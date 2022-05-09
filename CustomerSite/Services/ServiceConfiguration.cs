using CustomerSite.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerSite.Services
{
    public static class ServiceConfiguration
    {
        public static void AddServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ISharedService, SharedService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
        }   
    }
}