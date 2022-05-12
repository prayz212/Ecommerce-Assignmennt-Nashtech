using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Admin
{
    [Authorize(Roles = UserRoles.ADMIN)]
    [ApiController]
    [Route("api/admin/accounts")]
    public class AccountController  : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetAllClients([FromQuery] int page = ConstantVariable.DEFAULT_ACCOUNT_PAGE_NUMBER, [FromQuery] int size = ConstantVariable.DEFAULT_ACCOUNT_SIZE_PER_PAGE)
        {
            if (page < 1 || size < 1) return BadRequest();
            var clients = await _accountService.GetAccounts(page, size);
            return Ok(clients);
        }
    }
}