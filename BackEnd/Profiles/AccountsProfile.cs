using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.ViewModels;

namespace BackEnd.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<ApplicationUser, AccountDto>()
                .ForMember(des => des.Username,
                    opts => opts.MapFrom(src => src.UserName));
        }
    }
}