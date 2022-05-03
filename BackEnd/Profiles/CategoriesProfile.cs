using System.Collections.Generic;
using System.Linq;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using AutoMapper;

namespace BackEnd.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDetailDto>();
        }
    }
}