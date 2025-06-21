using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballGameDtos;
using SportsLeagueApi.Services.BaseService;
using sportsLeagueApi.Dtos.Basketball.BasketballGameDtos;

namespace SportsLeagueApi.Services.Basketball.BasketballGameService
{
    public interface IBasketballGameService : IBaseService<BasketballGame>
    {   Task<IEnumerable<BasketballGame>> GetAllBasketballGames();
        Task<BasketballGame> GetBasketballGameById(int id);
        Task<BasketballGame> CreateBasketballGame(CreateBasketballGameDto gameDto);
        Task<BasketballGame> UpdateBasketballGame(int id, UpdateBasketballGameDto gameDto);
        Task<bool> DeleteBasketballGame(int id);
    }
}