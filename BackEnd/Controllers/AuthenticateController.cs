using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Utils;
using Microsoft.AspNetCore.Mvc;
using Shared.Clients;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login/client")]
        public async Task<IActionResult> ClientLogin(LoginDto dto)
        {
            var result = await _authenticateService.Login(dto, UserRoles.CLIENT);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> AdminLogin(LoginDto dto)
        {
            var result = await _authenticateService.Login(dto, UserRoles.ADMIN);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpPost("register/client")]
        public async Task<IActionResult> ClientRegister(ClientRegisterDto dto)
        {
            var result = await _authenticateService.ClientRegister(dto);
            return result ? Ok() : BadRequest();
        }

        // [HttpPost("register/admin")]
        // public async Task<IActionResult> AdminRegister(RegisterDto dto)
        // {
        //     var result = await _authenticateService.Register(dto, UserRoles.ADMIN);
        //     return result ? Ok() : BadRequest();
        // }
    }
}