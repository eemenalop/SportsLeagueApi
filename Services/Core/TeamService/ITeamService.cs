using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.TeamDtos;

namespace SportsLeagueApi.Services.Core.TeamService
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task<TeamResponseDto> CreateTeam(CreateTeamDto teamDto);
        Task<TeamResponseDto> UpdateTeam(int id, UpdateTeamDto teamDto);
        Task<bool> DeleteTeam(int id);
    }      
}