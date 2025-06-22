using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Basketball.PlayerBasketballStatsService;
using SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos;
using Microsoft.AspNetCore.Mvc;

namespace SportsLeagueApi.Controllers.Basketball.PlayerBasketballStats
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerBasketballStatsController : Controller
    {
        private readonly IPlayerBasketballStatsService _playerBasketballStats;
        public PlayerBasketballStatsController(IPlayerBasketballStatsService playerBasketballStats)
        {
            _playerBasketballStats = playerBasketballStats;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlayerBasketballStats()
        {
            try
            {
                var stats = await _playerBasketballStats.GetAllPlayerBasketballStats();
                return Ok(stats);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound("No player basketball stats found: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving player basketball stats: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerBasketballStatsById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid stats ID.");
                var stats = await _playerBasketballStats.GetPlayerBasketballStatsById(id);
                if (stats == null)
                {
                    return NotFound($"Player basketball stats with ID {id} not found.");
                }
                return Ok(stats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Player basketball stats not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the basketball stats: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerBasketballStats([FromBody] CreatePlayerBasketballStatsDto basketStatsDto)
        {
            try
            {
                if (basketStatsDto == null)
                    throw new ArgumentException("Basketball stats data cannot be null.");
                var stats = await _playerBasketballStats.CreatePlayerBasketballStats(basketStatsDto);
                return CreatedAtAction(nameof(GetPlayerBasketballStatsById), new { id = stats.Id }, stats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the basketball stats: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerBasketballStats(int id, [FromBody] UpdatePlayerBasketballStatsDto basketStatsDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid stats ID.");
                if (basketStatsDto == null)
                    throw new ArgumentException("Basketball stats data cannot be null.");
                var updatedStats = await _playerBasketballStats.UpdatePlayerBasketballStats(id, basketStatsDto);
                if (updatedStats == null)
                {
                    return NotFound($"Player basketball stats with ID {id} not found.");
                }
                return Ok(updatedStats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Player basketball stats not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the basketball stats: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerBasketballStats(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid stats ID.");
                var result = await _playerBasketballStats.DeletePlayerBasketballStats(id);
                if (!result)
                {
                    return NotFound($"Player basketball stats with ID {id} not found.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Player basketball stats not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the basketball stats: " + ex.Message);
            }
        }

    } 
}