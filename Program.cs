using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Services.AccountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IAccountService, AccountService>();
DatabaseConfig.ConfigureDbContext(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
