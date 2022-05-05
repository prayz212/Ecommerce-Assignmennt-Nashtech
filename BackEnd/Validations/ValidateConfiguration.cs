using BackEnd.Models.ViewModels;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Validations
{
    public static class ValidateConfiguration
    {
        public static void AddValidatesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
            services.AddScoped<IValidator<CategoryDetailDto>, UpdateCategoryValidator>();
            services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductValidator>();
        }
    }
}