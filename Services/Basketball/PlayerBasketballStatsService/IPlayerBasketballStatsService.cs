using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos;

namespace SportsLeagueApi.Services.Basketball.PlayerBasketballStatsService
{
    public interface IPlayerBasketballStatsService
    {
        Task<IEnumerable<PlayerBasketballStat>> GetAllPlayerBasketballStats();
        Task<PlayerBasketballStat> GetPlayerBasketballStatsById(int id);
        Task<PlayerBasketballStat> CreatePlayerBasketballStats(CreatePlayerBasketballStatsDto basketStatsDto);
        Task<PlayerBasketballStat> UpdatePlayerBasketballStats(int id, UpdatePlayerBasketballStatsDto basketStatsDto);
        Task<bool> DeletePlayerBasketballStats(int id);
    }
}