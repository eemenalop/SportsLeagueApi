using SportsLeagueApi.Data;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.AccountDtos;

namespace SportsLeagueApi.Services.AccountService
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<Account> CreateAccount(CreateAccountDto accountDto);
        Task<Account> UpdateAccount(int id, UpdateAccountDto accountDto);
        Task<bool> DeleteAccount(int id);
    }
}
