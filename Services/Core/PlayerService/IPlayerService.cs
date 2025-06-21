using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.PlayerDtos;

namespace SportsLeagueApi.Services.Core.PlayerService
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<PlayerResponseDto> GetPlayerById(int id);
        Task<Player> CreatePlayer(CreatePlayerDto playerDto);
        Task<Player> UpdatePlayer(int id, UpdatePlayerDto playerDto);
        Task<bool> DeletePlayer(int id);
    }
}