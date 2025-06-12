using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.LeagueDtos;

namespace SportsLeagueApi.Services.LeagueService
{
    public interface ILeagueService : IBaseService<League>
    {
        Task<League> CreateLeague(CreateLeagueDto leagueDto);
        Task<League> UpdateLeague(int id, UpdateLeagueDto leagueDto);
        Task<bool> DeleteLeague(int id);
    }
}