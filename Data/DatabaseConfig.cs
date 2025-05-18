using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Data;

public static class DatabaseConfig
{
    public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}
