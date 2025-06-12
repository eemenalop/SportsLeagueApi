using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.AccountDtos;
using SportsLeagueApi.Dtos.LeagueDtos;

namespace SportsLeagueApi.Services.AccountService
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly AppDbContext _accountContext;
        public AccountService(AppDbContext context) : base(context)
        {
            _accountContext = context;
        }

        private void ValidateAccountDto(IAccountDto accountDto)
        {
            if (accountDto == null)
                throw new ArgumentNullException(nameof(accountDto), "Account data cannot be null.");
            if (string.IsNullOrWhiteSpace(accountDto.Name))
                throw new ArgumentException("Name is required.");
            if (!char.IsUpper(accountDto.Name[0]))
                throw new ArgumentException("Name must start with an uppercase letter.");
            if (string.IsNullOrWhiteSpace(accountDto.LastName))
                throw new ArgumentException("Last name is required.");
            if (!char.IsUpper(accountDto.LastName[0]))
                throw new ArgumentException("Last name must start with an uppercase letter.");
            if (string.IsNullOrWhiteSpace(accountDto.Email))
                throw new ArgumentException("Email is required.");
            if (!accountDto.Email.Contains("@"))
                throw new ArgumentException("Invalid email format.");
            if (string.IsNullOrWhiteSpace(accountDto.Password))
                throw new ArgumentException("Password is required.");
            if (accountDto.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");
            if (accountDto.Password.Length > 100)
                throw new ArgumentException("Password must not exceed 100 characters.");
            if (accountDto.RoleId <= 0)
                throw new ArgumentException("Role ID must be greater than zero.");
            if (string.IsNullOrWhiteSpace(accountDto.DocumentId))
                throw new ArgumentException("Document ID is required.");
            if (!accountDto.DocumentId.All(char.IsDigit))
                throw new ArgumentException("Document ID must contain only digits.");
            if (!accountDto.Phone.All(char.IsDigit))
                throw new ArgumentException("Phone must contain only digits.");
            if (accountDto.DateOfBirth == default)
                throw new ArgumentException("Date of birth is required.");
            if (accountDto.DateOfBirth != null && accountDto.DateOfBirth > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future.");
            if (accountDto.DateOfBirth != null && accountDto.DateOfBirth < DateTime.Now.AddYears(-120))
                throw new ArgumentException("Date of birth is not valid.");
        }

        public async Task<Account> CreateAccount(CreateAccountDto accountDto)
        {
            ValidateAccountDto(accountDto);
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
            ValidateAccountDto(accountDto);
            if (id <= 0)
            {
                throw new ArgumentException("Invalid league ID provided.");
            }
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

        public async Task<bool> DeleteAccount(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid account ID provided.");
            }
            var account = await _accountContext.Accounts.FindAsync(id);
            if (account == null)
            {
                return false;
            }
            _accountContext.Accounts.Remove(account);
            await _accountContext.SaveChangesAsync();
            return true;
        }
    }
}
