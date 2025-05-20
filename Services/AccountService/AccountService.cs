using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.AccountDtos;


namespace SportsLeagueApi.Services.AccountService
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly AppDbContext _accountContext;
        public AccountService(AppDbContext context) : base(context)
        {
            _accountContext = context;
        }

        public async Task<Account> CreateAccount(CreateAccountDto account)
        {
            var newAccount = new Account
            {
                Name = account.Name,
                LastName = account.LastName,
                Email = account.Email,
                Password = account.Password,
                RoleId = account.RoleId,
                DocumentId = account.DocumentId,
                Phone = account.Phone,
                DateOfBirth = account.DateOfBirth
            };

            await _accountContext.Accounts.AddAsync(newAccount);
            await _accountContext.SaveChangesAsync();
            return newAccount;
        }
    }
}
