using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;

namespace SportsLeagueApi.Services.Core.TeamService
{
    public interface ITeamService : IBaseService<Team>
    {
        Task<TeamResponseDto> CreateTeam(CreateTeamDto teamDto);
        Task<TeamResponseDto> UpdateTeam(int id, UpdateTeamDto teamDto);
        Task<bool> DeleteTeam(int id);
    }      
}