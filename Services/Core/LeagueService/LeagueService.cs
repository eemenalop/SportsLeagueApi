using SportsLeagueApi.Models;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.Core.LeagueDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace SportsLeagueApi.Services.Core.LeagueService
{
    public class LeagueService : ILeagueService
    {
        private readonly AppDbContext _leagueContext;

        public LeagueService(AppDbContext context)
        {
            _leagueContext = context;
        }
        private void ValidateLeagueDto(ILeagueDto leagueDto)
        {
            if (leagueDto == null)
            {
                throw new ArgumentException("League data is required.");
            }
            if (string.IsNullOrWhiteSpace(leagueDto.Name))
            {
                throw new ArgumentException("Invalid league name provided.");
            }
            if (leagueDto.SportId <= 0)
            {
                throw new ArgumentException("Invalid sport ID provided.");
            }
            if (leagueDto.AdminId <= 0)
            {
                throw new ArgumentException("Invalid admin ID provided.");
            }
        }
        public async Task<IEnumerable<League>> GetAllLeagues()
        {
            if (_leagueContext.Leagues == null)
            {
                throw new ArgumentException("No leagues found in the database.");
            }
            return await _leagueContext.Leagues.Include(t=>t.Tournaments).Include(u=>u.UserLeagues).ToListAsync();
        }
        public async Task<League> GetLeagueById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid league ID provided.");
            }
            var league = await _leagueContext.Leagues.FindAsync(id);
            if (league == null)
            {
                throw new KeyNotFoundException($"League with ID {id} not found");
            }
            return league;
        }
        public async Task<League> CreateLeague(CreateLeagueDto leagueDto)
        {
            ValidateLeagueDto(leagueDto);
            var newLeague = new League
            {
                Name = leagueDto.Name,
                SportId = leagueDto.SportId,
                AdminId = leagueDto.AdminId
            };

            await _leagueContext.Leagues.AddAsync(newLeague);
            await _leagueContext.SaveChangesAsync();
            return newLeague;
        }
        public async Task<League> UpdateLeague(int id, UpdateLeagueDto leagueDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid league ID provided.");
            }

            ValidateLeagueDto(leagueDto);
            
            var existingLeague = await _leagueContext.Leagues.FindAsync(id);
            if (existingLeague == null)
            {
                throw new KeyNotFoundException($"League with ID {id} not found");
            }
            existingLeague.Name = leagueDto.Name;
            existingLeague.SportId = leagueDto.SportId;
            existingLeague.AdminId = leagueDto.AdminId;
            await _leagueContext.SaveChangesAsync();
            return existingLeague;
        }
        public async Task<bool> DeleteLeague(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid league ID provided.");
            }
            var league = await _leagueContext.Leagues.FindAsync(id);
            if (league == null)
            {
                throw new KeyNotFoundException($"League with ID {id} not found");
            }
            _leagueContext.Leagues.Remove(league);
            await _leagueContext.SaveChangesAsync();
            return true;
        }

    }
}