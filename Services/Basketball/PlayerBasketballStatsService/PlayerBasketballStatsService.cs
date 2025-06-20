using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos;
using SportsLeagueApi.Data;
using System.Reflection;

namespace SportsLeagueApi.Services.Basketball.PlayerBasketballStatsService
{
    public class PlayerBasketballStatsService : BaseService<PlayerBasketballStat>, IPlayerBasketballStatsService
    {
        private readonly AppDbContext _playerBasketballStatsContext;

        public PlayerBasketballStatsService(AppDbContext playerBasketballStatsContext) : base(playerBasketballStatsContext)
        {
            _playerBasketballStatsContext = playerBasketballStatsContext;
        }
        private void ValidateBasketballStats(IPlayerBasketballStatsDto basketStatsDto)
        {
            if (basketStatsDto == null)
            {
                throw new ArgumentNullException(nameof(basketStatsDto), "Stats DTO cannot be null.");
            }
            if (basketStatsDto.PlayerId <= 0)
            {
                throw new ArgumentException("PlayerId must be greater than 0.");
            }
            if (basketStatsDto.GameId <= 0)
            {
                throw new ArgumentException("GameId must be greater than 0.");
            }
            foreach (PropertyInfo prop in basketStatsDto.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(int))
                {
                    object val = prop.GetValue(basketStatsDto);
                    int? value = val as int?;
                    if (value.HasValue)
                    {
                        if (value.Value < 0)
                            throw new ArgumentException($"{prop.Name} cannot be negative.");
                    }
                }
            }
        }
        public async Task<PlayerBasketballStat> CreatePlayerBasketballStats(CreatePlayerBasketballStatsDto basketStatsDto)
        {
            ValidateBasketballStats(basketStatsDto);
            PlayerBasketballStat newBasketballStat = new PlayerBasketballStat
            {
                PlayerId = basketStatsDto.PlayerId,
                GameId = basketStatsDto.GameId,
                Points = basketStatsDto.Points,
                Rebounds = basketStatsDto.Rebounds,
                Assists = basketStatsDto.Assists,
                Steals = basketStatsDto.Steals,
                Blocks = basketStatsDto.Blocks,
                Fga = basketStatsDto.Fga,
                Fgm = basketStatsDto.Fgm,
                Threepta = basketStatsDto.Threepta,
                Threeptm = basketStatsDto.Threeptm,
                Fta = basketStatsDto.Fta,
                Ftm = basketStatsDto.Ftm,
                Fouls = basketStatsDto.Fouls,
                Turnovers = basketStatsDto.Turnovers
            };
            await _playerBasketballStatsContext.PlayerBasketballStats.AddAsync(newBasketballStat);
            await _playerBasketballStatsContext.SaveChangesAsync();
            return newBasketballStat;
        }
        public async Task<PlayerBasketballStat> UpdatePlayerBasketballStats(int id, UpdatePlayerBasketballStatsDto basketStatsDto)
        {
            ValidateBasketballStats(basketStatsDto);
            PlayerBasketballStat? existingStat = await _playerBasketballStatsContext.PlayerBasketballStats.FindAsync(id);
            if (existingStat == null)
            {
                throw new KeyNotFoundException($"PlayerBasketballStat with ID {id} not found.");
            }
            existingStat.PlayerId = basketStatsDto.PlayerId;
            existingStat.GameId = basketStatsDto.GameId;
            existingStat.Points = basketStatsDto.Points;
            existingStat.Rebounds = basketStatsDto.Rebounds;
            existingStat.Assists = basketStatsDto.Assists;
            existingStat.Steals = basketStatsDto.Steals;
            existingStat.Blocks = basketStatsDto.Blocks;
            existingStat.Fga = basketStatsDto.Fga;
            existingStat.Fgm = basketStatsDto.Fgm;
            existingStat.Threepta = basketStatsDto.Threepta;
            existingStat.Threeptm = basketStatsDto.Threeptm;
            existingStat.Fta = basketStatsDto.Fta;
            existingStat.Ftm = basketStatsDto.Ftm;
            existingStat.Fouls = basketStatsDto.Fouls;
            existingStat.Turnovers = basketStatsDto.Turnovers;
            _playerBasketballStatsContext.PlayerBasketballStats.Update(existingStat);
            _playerBasketballStatsContext.SaveChanges();
            return existingStat;
        }
        public async Task<bool> DeletePlayerBasketballStats(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than 0.");
            }
            PlayerBasketballStat? existingStat = await _playerBasketballStatsContext.PlayerBasketballStats.FindAsync(id);
            if (existingStat == null)
            {
                throw new KeyNotFoundException($"PlayerBasketballStat with ID {id} not found.");
            }
            _playerBasketballStatsContext.PlayerBasketballStats.Remove(existingStat);
            await _playerBasketballStatsContext.SaveChangesAsync();
            return true;
        }

    }
}