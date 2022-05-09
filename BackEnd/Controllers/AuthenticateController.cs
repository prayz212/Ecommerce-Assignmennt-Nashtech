using System.Threading.Tasks;
using BackEnd.Interfaces;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authenticateService.Login(dto);
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