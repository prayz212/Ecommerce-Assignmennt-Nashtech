using AutoMapper;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<ClientRegisterDto, ApplicationUser>();
            // CreateMap<AdminRegisterDto, ApplicationUser>();
            CreateMap<LoginDto, ApplicationUser>();
        }
    }
}