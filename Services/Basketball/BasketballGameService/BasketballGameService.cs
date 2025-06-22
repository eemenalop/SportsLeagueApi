using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Basketball.BasketballGameDtos;
using SportsLeagueApi.Services.Basketball.BasketballGameService;
using SportsLeagueApi.Data;
using sportsLeagueApi.Dtos.Basketball.BasketballGameDtos;
using Microsoft.EntityFrameworkCore;

namespace SportsLeagueApi.Services.Basketball.BasketballGameService
{
    public class BasketballGameService : IBasketballGameService
    {
        private readonly AppDbContext _basketballGameContext;

        public BasketballGameService(AppDbContext basketballGameContext)
        {
            _basketballGameContext = basketballGameContext;
        }

        private void ValidateBasketballGame(IBasketballGameDto gameDto)
        {
            if (gameDto == null)
            {
                throw new ArgumentNullException(nameof(gameDto), "Game cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(gameDto.TeamA.ToString()) || string.IsNullOrWhiteSpace(gameDto.TeamB.ToString()))
            {
                throw new ArgumentException("Home and Away team names cannot be empty.");
            }
            if (gameDto.ScoreTeamA < 0 || gameDto.ScoreTeamB < 0)
            {
                throw new ArgumentException("Scores cannot be negative.");
            }
            if (gameDto.TournamentId <= 0)
            {
                throw new ArgumentException("Invalid tournament ID.");
            }
            if (string.IsNullOrWhiteSpace(gameDto.TournamentId.ToString()))
            {
                throw new ArgumentException("Tournament ID cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(gameDto.GameType))
            {
                throw new ArgumentException("Game type cannot be empty.");
            }
            if (gameDto.GameType != "Regular" && gameDto.GameType != "Playoff" && gameDto.GameType != "Finals")
            {
                throw new ArgumentException("Must be 'Regular', 'Playoff' or 'Finals'.");
            }
            if (gameDto.State != "Scheduled" && gameDto.State != "InProgress" && gameDto.State != "Completed" && gameDto.State != "Suspended")
            {
                throw new ArgumentException("Invalid game state. Must be 'Scheduled', 'InProgress', 'Suspended' or 'Completed'.");
            }
        }
        public async Task<IEnumerable<BasketballGame>> GetAllBasketballGames()
        {
            if (_basketballGameContext.BasketballGames == null)
            {
                throw new KeyNotFoundException("No basketball games found in the database.");
            }
            return await _basketballGameContext.BasketballGames.ToListAsync();
        }
        public async Task<BasketballGame> GetBasketballGameById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid game ID provided.", nameof(id));
            }
            var basketballGame = await _basketballGameContext.BasketballGames.FindAsync(id);
            if (basketballGame == null)
            {
                throw new KeyNotFoundException($"Basketball game with ID {id} not found.");
            }
            return basketballGame;
        }

        public async Task<BasketballGame> CreateBasketballGame(CreateBasketballGameDto gameDto)
        {
            ValidateBasketballGame(gameDto);
            var basketballGame = new BasketballGame
            {
                TeamA = gameDto.TeamA,
                TeamB = gameDto.TeamB,
                ScoreTeamA = gameDto.ScoreTeamA,
                ScoreTeamB = gameDto.ScoreTeamB,
                TournamentId = gameDto.TournamentId,
                GameType = gameDto.GameType,
                State = gameDto.State
            };

            await _basketballGameContext.BasketballGames.AddAsync(basketballGame);
            await _basketballGameContext.SaveChangesAsync();

            return basketballGame;
        }
        public async Task<BasketballGame> UpdateBasketballGame(int id, UpdateBasketballGameDto gameDto)
        {
            ValidateBasketballGame(gameDto);
            if (id <= 0)
            {
                throw new ArgumentException("Invalid game ID.");
            }
            var basketballGame = await _basketballGameContext.BasketballGames.FindAsync(id);
            if (basketballGame == null)
            {
                throw new KeyNotFoundException($"Basketball game with ID {id} not found.");
            }

            basketballGame.TeamA = gameDto.TeamA;
            basketballGame.TeamB = gameDto.TeamB;
            basketballGame.ScoreTeamA = gameDto.ScoreTeamA;
            basketballGame.ScoreTeamB = gameDto.ScoreTeamB;
            basketballGame.TournamentId = gameDto.TournamentId;
            basketballGame.GameType = gameDto.GameType;
            basketballGame.State = gameDto.State;

            _basketballGameContext.BasketballGames.Update(basketballGame);
            await _basketballGameContext.SaveChangesAsync();

            return basketballGame;
        }
        public async Task<bool> DeleteBasketballGame(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid game ID.");
            }
            var basketballGame = await _basketballGameContext.BasketballGames.FindAsync(id);
            if (basketballGame == null)
            {
                throw new KeyNotFoundException($"Basketball game with ID {id} not found.");
            }

            _basketballGameContext.BasketballGames.Remove(basketballGame);
            await _basketballGameContext.SaveChangesAsync();
            return true;
        }
    }
}