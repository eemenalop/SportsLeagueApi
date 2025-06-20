using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.PlayerDtos;

namespace SportsLeagueApi.Services.Core.PlayerService
{
    public interface IPlayerService : IBaseService<Player>
    {
        Task<Player> CreatePlayer(CreatePlayerDto playerDto);
        Task<Player> UpdatePlayer(int id, UpdatePlayerDto playerDto);
        Task<bool> DeletePlayer(int id);
    }
}