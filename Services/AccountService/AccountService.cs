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

        public async Task<Account> CreateAccount(CreateAccountDto accountDto)
        {
            var newAccount = new Account
            {
                Name = accountDto.Name,
                LastName = accountDto.LastName,
                Email = accountDto.Email,
                Password = accountDto.Password,
                RoleId = accountDto.RoleId,
                DocumentId = accountDto.DocumentId,
                Phone = accountDto.Phone,
                DateOfBirth = accountDto.DateOfBirth
            };

            await _accountContext.Accounts.AddAsync(newAccount);
            await _accountContext.SaveChangesAsync();
            return newAccount;
        }

        public async Task<Account> UpdateAccount(int id, UpdateAccountDto accountDto)
        {
            var existingUser = await _accountContext.Accounts.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            existingUser.Name = accountDto.Name;
            existingUser.LastName = accountDto.LastName;
            existingUser.Email = accountDto.Email;
            existingUser.Password = accountDto.Password;
            existingUser.RoleId = accountDto.RoleId;
            existingUser.DocumentId = accountDto.DocumentId;
            existingUser.Phone = accountDto.Phone;
            existingUser.DateOfBirth = accountDto.DateOfBirth;
            await _accountContext.SaveChangesAsync();
            return existingUser;

        }
    }
}
