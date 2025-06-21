using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Services.Core.SportService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.Core.SportDtos;

namespace SportsLeagueApi.Services.Core.SportService
{
    public class SportService : ISportService
    {
        private readonly AppDbContext _sportContext;

        public SportService(AppDbContext context)
        {
            _sportContext = context;
        }
        private void ValidateSportDto(ISportDto sportDto)
        {
            if (sportDto == null)
            {
                throw new ArgumentNullException(nameof(sportDto), "Sport data cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(sportDto.Name))
            {
                throw new ArgumentException("Sport name cannot be empty.", nameof(sportDto.Name));
            }
            if (sportDto.Name.Length < 3)
            {
                throw new ArgumentException("Sport name must be at least 3 characters long.", nameof(sportDto.Name));
            }
            if (sportDto.Name.Length > 50)
            {
                throw new ArgumentException("Sport name must not exceed 50 characters.", nameof(sportDto.Name));
            }
            if (!char.IsUpper(sportDto.Name[0]))
            {
                throw new ArgumentException("Sport name must start with an uppercase letter.", nameof(sportDto.Name));
            }
        }
        public async Task<IEnumerable<Sport>> GetAllSports()
        {
            if (_sportContext.Sports == null)
            {
                throw new ArgumentException("Sports context is not initialized.");
            }
            return await _sportContext.Sports.ToListAsync();
        }
        public async Task<Sport> GetSportById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid sport ID provided.", nameof(id));
            }
            var sport = await _sportContext.Sports.FindAsync(id);
            if (sport == null)
            {
                throw new KeyNotFoundException($"Sport with ID {id} not found.");
            }
            return sport;
        }
        public async Task<Sport> CreateSport(CreateSportDto sportDto)
        {
            ValidateSportDto(sportDto);
            if (await _sportContext.Sports.AnyAsync(s => s.Name == sportDto.Name))
            {
                throw new ArgumentException($"Sport with name {sportDto.Name} already exists.");
            }
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
            ValidateSportDto(sportDto);
            if (await _sportContext.Sports.AnyAsync(s => s.Name == sportDto.Name && s.Id != id))
            {
                throw new ArgumentException($"Sport with name {sportDto.Name} already exists.");
            }
            if (id <= 0)
            {
                throw new ArgumentException("Invalid sport ID provided.", nameof(id));
            }
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid sport ID provided.", nameof(id));
            }
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