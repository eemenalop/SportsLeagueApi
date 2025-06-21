using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.TeamService;
using SportsLeagueApi.Dtos.Core.TeamDtos;
using SportsLeagueApi.Services.BaseService;

namespace SportsLeagueApi.Controllers.Core.TeamController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var teams = await _teamService.GetAllTeams();
                return Ok(teams);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Error retrieving teams: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while retrieving the teams: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid team ID.");
                var team = await _teamService.GetTeamById(id);
                if (team == null)
                {
                    return NotFound($"Team with ID {id} not found.");
                }
                return Ok(team);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Team with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while retrieving the team: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto teamDto)
        {
            try
            {
                if (teamDto == null)
                    throw new ArgumentException("Team data cannot be null.");
                var team = await _teamService.CreateTeam(teamDto);
                return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while creating the team: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] UpdateTeamDto teamDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid team ID.");
                if (teamDto == null)
                    throw new ArgumentException("Team data cannot be null.");

                var updatedTeam = await _teamService.UpdateTeam(id, teamDto);
                if (updatedTeam == null)
                {
                    return NotFound($"Team with ID {id} not found.");
                }
                return Ok(updatedTeam);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while updating the team: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid team ID.");

                var result = await _teamService.DeleteTeam(id);
                if (!result)
                {
                    return NotFound($"Team with ID {id} not found.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Team not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while deleting the team: " + ex.Message);
            }
        }
    }
}