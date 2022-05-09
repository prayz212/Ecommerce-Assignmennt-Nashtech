using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Clients;

namespace CustomerSite.Validations
{
    public static class ValidateConfiguration
    {
        public static void AddValidatesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            // services.AddScoped<IValidator<ClientRegisterDto>, ClientRegisterValidator>();
        }
    }
}