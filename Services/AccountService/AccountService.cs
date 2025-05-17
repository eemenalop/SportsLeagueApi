using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Data;


namespace SportsLeagueApi.Services.AccountService
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public AccountService(AppDbContext context) : base(context) { }

        
    }
}
