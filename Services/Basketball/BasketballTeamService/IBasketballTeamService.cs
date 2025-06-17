using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;

namespace SportsLeagueApi.Services.Basketball.BasketballTeamService
{
    public interface IBasketballTeamService : IBaseService<BasketballTeam>
    {
        Task<BasketballTeam> CreateBasketballTeam(CreateBasketballTeamDto teamDto);
        Task<BasketballTeam> UpdateBasketballTeam(int id, UpdateBasketballTeamDto teamDto);
        Task<bool> DeleteBasketballTeam(int id);
    }      
}