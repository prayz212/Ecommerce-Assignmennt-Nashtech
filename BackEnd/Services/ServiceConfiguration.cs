using BackEnd.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Services
{
    public static class ServiceConfiguration
    {
        public static void AddServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }   
    }
}