using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.LeagueDtos;

namespace SportsLeagueApi.Services.Core.LeagueService
{
    public interface ILeagueService
    {   Task<IEnumerable<League>> GetAllLeagues();
        Task<League> GetLeagueById(int id);
        Task<League> CreateLeague(CreateLeagueDto leagueDto);
        Task<League> UpdateLeague(int id, UpdateLeagueDto leagueDto);
        Task<bool> DeleteLeague(int id);
    }
}