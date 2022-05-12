using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Clients;

namespace BackEnd.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<TokenDto> Login(LoginDto login, string loginRole)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user is not null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var isAuthorize = await _userManager.IsInRoleAsync(user, loginRole);
                if (!isAuthorize) return null;

                var userRoles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                };

                foreach(var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return this.GenerateToken(claims);
            }

            return null;
        }

        public async Task<bool> ClientRegister(ClientRegisterDto register)
        {
            var exist = await _userManager.FindByNameAsync(register.UserName);
            if (exist is not null) return false;

            var newUser = _mapper.Map<ApplicationUser>(register);
            var result = await _userManager.CreateAsync(newUser, register.Password);
            if (result.Succeeded) await this.AddNewUserToRoleAsync(newUser, UserRoles.CLIENT);
            
            return result.Succeeded;
        }

        private async Task AddNewUserToRoleAsync(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.ADMIN))  
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));  

            if (!await _roleManager.RoleExistsAsync(UserRoles.CLIENT))  
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.CLIENT)); 

            await _userManager.AddToRoleAsync(user, role);
        }

        private TokenDto GenerateToken(IList<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo.ToLocalTime(),
            };
        }
    }
}