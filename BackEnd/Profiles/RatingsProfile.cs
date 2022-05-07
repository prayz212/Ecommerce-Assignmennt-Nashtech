
using AutoMapper;
using BackEnd.Models;
using Shared.Clients;

namespace Namespace
{
    public class RatingsProfile : Profile
    {
        public RatingsProfile()
        {
            CreateMap<ProductRatingWriteDto, Rating>();
        }
    }
}