using SportsLeagueApi.Data;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.AccountDtos;

namespace SportsLeagueApi.Services.Core.AccountService
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<AccountResponseDto> GetAccountById(int id);
        Task<Account> CreateAccount(CreateAccountDto accountDto);
        Task<Account> UpdateAccount(int id, UpdateAccountDto accountDto);
        Task<bool> DeleteAccount(int id);
    }
}
