using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos;

namespace SportsLeagueApi.Services.Basketball.PlayerBasketballStatsService
{
    public interface IPlayerBasketballStatsService : IBaseService<PlayerBasketballStat>
    {
        Task<PlayerBasketballStat> CreatePlayerBasketballStats(CreatePlayerBasketballStatsDto basketStatsDto);
        Task<PlayerBasketballStat> UpdatePlayerBasketballStats(int id, UpdatePlayerBasketballStatsDto basketStatsDto);
        Task<bool> DeletePlayerBasketballStats(int id);
    }
}