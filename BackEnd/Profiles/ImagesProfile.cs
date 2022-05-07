using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.ViewModels;

namespace BackEnd.Profiles
{
    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<ImageDto, Image>()
                .ForMember(des => des.ProductId, opts => opts.Ignore());
        }
    }
}