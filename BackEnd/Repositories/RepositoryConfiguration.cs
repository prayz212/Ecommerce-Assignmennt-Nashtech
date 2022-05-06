using BackEnd.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Repositories
{
    public static class RepositoryConfiguration
    {
        public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}