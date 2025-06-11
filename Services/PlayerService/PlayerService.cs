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

        public async Task<Player> CreatePlayer(CreatePlayerDto playerDto)
        {
            var newPlayer = new Player
            {
                Name = playerDto.Name,
                LastName = playerDto.LastName,
                DocumentId = playerDto.DocumentId,
                Position = playerDto.Position
            };
            await _playerContext.Players.AddAsync(newPlayer);
            await _playerContext.SaveChangesAsync();
            return newPlayer;
        }

        public async Task<Player> UpdatePlayer(int id, UpdatePlayerDto playerDto)
        {
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