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
        public async Task<Sport> CreateSport(CreateSportDto sportDto)
        {
            var newSport = new Sport
            {
                Name = sportDto.Name
            };
            await _sportContext.Sports.AddAsync(newSport);
            await _sportContext.SaveChangesAsync();
            return newSport;
        }
        public async Task<Sport> UpdateSport(int id, UpdateSportDto sportDto)
        {
            var existingSport = await _sportContext.Sports.FindAsync(id);
            if (existingSport == null)
            {
                throw new KeyNotFoundException($"Sport with ID {id} not found.");
            }
            existingSport.Name = sportDto.Name;
            await _sportContext.SaveChangesAsync();
            return existingSport;
        }
        public async Task<bool> DeleteSport(int id)
        {
            var sport = await _sportContext.Sports.FindAsync(id);
            if (sport == null)
            {
                throw new KeyNotFoundException($"Sport with ID {id} not found");
            }
            _sportContext.Remove(sport);    
            await _sportContext.SaveChangesAsync();
            return true;
        }
    }
}