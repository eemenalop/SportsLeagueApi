using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Data;
using Microsoft.AspNetCore.Authentication;
using SportsLeagueApi.Dtos.PlayerDtos;

namespace SportsLeagueApi.Services.PlayerService
{
    public class PlayerService : BaseService<Player>, IPlayerService
    {
        private readonly AppDbContext _playerContext;

        public PlayerService(AppDbContext context) : base(context)
        {
            _playerContext = context;
        }
        private void ValidatePlayerDto(IPlayerDto playerDto)
        {
            if (playerDto == null)
            {
                throw new ArgumentNullException(nameof(playerDto), "Player data cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(playerDto.Name))
            {
                throw new ArgumentException("Player name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(playerDto.LastName))
            {
                throw new ArgumentException("Player last name cannot be empty.");
            }
            if (playerDto.Name.Length > 50)
            {
                throw new ArgumentException("Player name must not exceed 50 characters.");
            }
            if (playerDto.LastName.Length > 50)
            {
                throw new ArgumentException("Player last name must not exceed 50 characters.");
            }
            if (!char.IsUpper(playerDto.Name[0]))
            {
                throw new ArgumentException("Player name must start with an uppercase letter.");
            }
            if (!char.IsUpper(playerDto.LastName[0]))
            {
                throw new ArgumentException("Player last name must start with an uppercase letter.");
            }
            if (string.IsNullOrWhiteSpace(playerDto.DocumentId))
            {
                throw new ArgumentException("Player document ID cannot be empty.");
            }
            if(!playerDto.DocumentId.All(char.IsDigit))
            {
                throw new ArgumentException("Player document ID must contain only digits.");
            }
        }
        public async Task<Player> CreatePlayer(CreatePlayerDto playerDto)
        {
            ValidatePlayerDto(playerDto);
            var newPlayer = new Player
            {
                Name = playerDto.Name,
                LastName = playerDto.LastName,
                DocumentId = playerDto.DocumentId,
                Position = playerDto.Position
            };
            await _playerContext.Players.AddAsync(newPlayer);
            await _playerContext.SaveChangesAsync();
            
            var userLeague = new UserLeague
            {
                PlayerId = newPlayer.Id,
                LeagueId = playerDto.LeagueId
            };
            await _playerContext.UserLeagues.AddAsync(userLeague);
            await _playerContext.SaveChangesAsync();
            return newPlayer;
        }

        public async Task<Player> UpdatePlayer(int id, UpdatePlayerDto playerDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid player ID.");
            }
            ValidatePlayerDto(playerDto);
            var existingPlayer = await _playerContext.Players.FindAsync(id);
            if (existingPlayer == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            existingPlayer.Name = playerDto.Name;
            existingPlayer.LastName = playerDto.LastName;
            existingPlayer.DocumentId = playerDto.DocumentId;
            existingPlayer.Position = playerDto.Position;
            await _playerContext.SaveChangesAsync();
            return existingPlayer;
        }

        public async Task<bool> DeletePlayer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid player ID.");
            }
            var existingPlayer = await _playerContext.Players.FindAsync(id);
            if (existingPlayer == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            _playerContext.Players.Remove(existingPlayer);
            await _playerContext.SaveChangesAsync();
            return true;
        }
    }
}