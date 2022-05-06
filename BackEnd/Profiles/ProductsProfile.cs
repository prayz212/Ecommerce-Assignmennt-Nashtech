using System;
using System.Linq;
using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDetailReadDto>()
                .ForMember(des => des.AverageRate, 
                    opts => opts.MapFrom(src => src.Ratings.FirstOrDefault() != null ? src.Ratings.Average(r => r.Stars) : 0))
                .ForMember(des => des.Images,
                    opts => opts.MapFrom(src => src.Images.Select(i => new ImageReadDto { Name = i.Name, Uri = i.Uri }).ToList<ImageReadDto>()));

            CreateMap<Product, ProductReadDto>()
                .ForMember(des => des.AverageRate, 
                    opts => opts.MapFrom(src => src.Ratings.FirstOrDefault() != null ? src.Ratings.Average(r => r.Stars) : 0))
                .ForMember(des => des.ThumbnailName, 
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault().Name))
                .ForMember(des => des.ThumbnailUri, 
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault().Uri));

            CreateMap<Product, ProductDto>()
                .ForMember(des => des.Category,
                    otps => otps.MapFrom(src => src.Category.DisplayName));
            
            CreateMap<Product, ProductDetailDto>()
                .ForMember(des => des.AverageRate,
                    opts => opts.MapFrom(src => src.Ratings.FirstOrDefault() != null ? src.Ratings.Average(r => r.Stars) : 0))
                .ForMember(des => des.Images,
                    opts => opts.MapFrom(src => src.Images.Select(i => new ImageDto { Name = i.Name, Uri = i.Uri, Format = i.Format, Height = i.Height, Width = i.Width, Size = i.Size }).ToList<ImageDto>()))
                .ForMember(des => des.Category,
                    opts => opts.MapFrom(src => src.Category.DisplayName))
                .ForMember(des => des.CreatedAt,
                    opts => opts.MapFrom(src => src.CreatedDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.UpdatedAt,
                    opts => opts.MapFrom(src => src.UpdatedDate.ToString("dd/MM/yyyy")));

            CreateMap<CreateProductDto, Product>()
                .ForMember(des => des.Category, opts => opts.Ignore())
                .ForMember(des => des.CategoryId,
                    opts => opts.MapFrom(src => src.Category))
                .ForMember(des => des.Images, opts => opts.Ignore())
                .ForMember(des => des.Ratings, opts => opts.Ignore());

            CreateMap<UpdateProductDto, Product>()
                .ForMember(des => des.Category, opts => opts.Ignore())
                .ForMember(des => des.CategoryId,
                    opts => opts.MapFrom(src => src.Category))
                .ForMember(des => des.Images, opts => opts.Ignore())
                .ForMember(des => des.Ratings, opts => opts.Ignore());
        }
    }
}