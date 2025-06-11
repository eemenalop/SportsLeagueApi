using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Services.AccountService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.AccountDtos;

namespace SportsLeagueApi.Controllers
{
    [Route("api/account")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] UpdateAccountDto account)
        {
            try
            {
                var updatedAccount = await _accountService.UpdateAccount(id, account);
                if (updatedAccount == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }
                return Ok(updatedAccount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("Error updating account");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await _accountService.GetById(id);
                if (account == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }
                await _accountService.DeleteAccount(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("Error deleting account");
            }
        }
    }
}