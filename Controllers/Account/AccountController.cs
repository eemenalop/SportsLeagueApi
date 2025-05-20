using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Services.AccountService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.AccountDtos;

namespace SportsLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account>
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) : base(accountService)
        {

            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto account)
        {
            try
            {
                var newAccount = await _accountService.CreateAccount(account);
                if (newAccount == null)
                {
                    return BadRequest("Error creating account");
                }
                return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);

            }
            catch
            {
                return BadRequest("Error creating account");
            }
        }
    }
}
