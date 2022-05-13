using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using BackEnd.Utils;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountListDto> GetAccounts(int page, int size)
        {
            var skip = (page - 1) * size;
            var allClients = await _userManager.GetUsersInRoleAsync(UserRoles.CLIENT);
            
            var count = allClients.Count;
            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawClients = allClients.Skip(skip).Take(size);
            var clients = _mapper.Map<IEnumerable<AccountDto>>(rawClients);

            return new AccountListDto
            {
                Accounts = clients,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0
            };
        }

        public async Task<AdminDetailDto> GetAccountInfo(string id)
        {
            var info = await _unitOfWork.Admins.GetByAccountId(id);
            return info is null
                ? null
                : _mapper.Map<AdminDetailDto>(info);
        }
    }
}