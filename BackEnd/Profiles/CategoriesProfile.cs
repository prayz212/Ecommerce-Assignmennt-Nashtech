using BackEnd.Models;
using BackEnd.Models.ViewModels;
using AutoMapper;
using Shared.Clients;

namespace BackEnd.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<Category, CategoryReadDto>();
        }
    }
}