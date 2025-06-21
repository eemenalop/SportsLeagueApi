using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Dtos.Core.PlayerDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.PlayerService;


namespace SportsLeagueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var players = await _playerService.GetAllPlayers();
                if (players == null || !players.Any())
                {
                    return NotFound("No players found.");
                }
                return Ok(players);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Error retrieving players: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while retrieving players: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            try
            {
                var player = await _playerService.GetPlayerById(id);
                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }
                return Ok(player);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while retrieving player: " + ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto playerDto)
        {
            try
            {
                var newPlayer = await _playerService.CreatePlayer(playerDto);
                if (newPlayer == null)
                {
                    return BadRequest("Error creating Player, missing or invalid player data");
                }
                return CreatedAtAction(nameof(GetPlayerById), new { id = newPlayer.Id }, newPlayer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while creating player: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] UpdatePlayerDto playerDto)
        {
            try
            {
                var player = await _playerService.UpdatePlayer(id, playerDto);
                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }
                return Ok(player);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while updating player: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = await _playerService.GetPlayerById(id);
                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }
                await _playerService.DeletePlayer(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while deleting player: " + ex.Message);
            }
        }
    }
}