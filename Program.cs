using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Services.Core.AccountService;
using SportsLeagueApi.Services.Core.RoleService;
using SportsLeagueApi.Services.Core.SportService;
using SportsLeagueApi.Services.Core.PlayerService;
using SportsLeagueApi.Services.Core.LeagueService;
using SportsLeagueApi.Services.Core.TournamentService;
using SportsLeagueApi.Services.Core.TeamService;
using SportsLeagueApi.Services.Basketball.BasketballGameService;
using SportsLeagueApi.Services.Basketball.PlayerBasketballStatsService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sports League API", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBasketballGameService, BasketballGameService>();
builder.Services.AddScoped<IPlayerBasketballStatsService, PlayerBasketballStatsService>();
DatabaseConfig.ConfigureDbContext(builder.Services, builder.Configuration);


var myCorsPolicy = "AllowSwagger";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCorsPolicy,
        policy =>
        {
            policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                  .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sports League API");
    });
}

app.UseHttpsRedirection();
app.UseCors(myCorsPolicy);
app.MapControllers();


app.Run();
