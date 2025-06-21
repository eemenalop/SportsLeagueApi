using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SportsLeagueApi.Dtos.Core.LeagueDtos;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.LeagueService;

namespace SportsLeagueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueController : Controller
    {
        private readonly ILeagueService _leagueService;
        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            try
            {
                var leagues = await _leagueService.GetAllLeagues();
                if (leagues == null || !leagues.Any())
                {
                    return NotFound("No leagues found.");
                }
                return Ok(leagues);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while retrieving leagues: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById(int id)
        {
            try
            {
                var league = await _leagueService.GetLeagueById(id);
                if (league == null)
                {
                    return NotFound($"League with ID {id} not found.");
                }
                return Ok(league);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while retrieving league: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto leagueDto)
        {
            try
            {
                var newLeague = await _leagueService.CreateLeague(leagueDto);
                if (newLeague == null)
                {
                    return BadRequest("Error creating league, missing or invalid league data.");
                }
                return CreatedAtAction(nameof(CreateLeague), new { id = newLeague.Id }, leagueDto);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while creating league: {ex.Message}");
            }
        }

        // PUT: api/League/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague(int id, [FromBody] UpdateLeagueDto leagueDto)
        {
            try
            {
                var updatedLeague = await _leagueService.UpdateLeague(id, leagueDto);
                if (updatedLeague == null)
                {
                    return NotFound($"League with ID {id} not found.");
                }
                return Ok(updatedLeague);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while updating league: {ex.Message}");
            }
        }

        // DELETE: api/League/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            try
            {
                var league = await _leagueService.DeleteLeague(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while deleting league: {ex.Message}");
            }
        }
    }
}