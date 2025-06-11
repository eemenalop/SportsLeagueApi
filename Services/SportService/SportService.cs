using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Services.SportService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.SportDtos;

namespace SportsLeagueApi.Services.SportService
{
    public class SportService : BaseService<Sport>, ISportService
    {
        private readonly AppDbContext _sportContext;

        public SportService(AppDbContext context) : base(context)
        {
            _sportContext = context;
        }
        public Task<Sport> CreateSport(CreateSportDto sportDto)
        {
            throw new NotImplementedException();
        }
        public Task<Sport> UpdateSport(int id, UpdateSportDto sportDto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteSport(int id)
        {
            throw new NotImplementedException();
        }
    }
}