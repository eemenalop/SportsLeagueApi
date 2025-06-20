using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.TeamService;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;
using SportsLeagueApi.Services.BaseService;

namespace SportsLeagueApi.Controllers.Core.TeamController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : BaseController<Team>
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService) : base(teamService)
        {
            _teamService = teamService;
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
                return StatusCode(500, "An error occurred while creating the basketball team: " + ex.Message);
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
                    return NotFound($"Basketball team with ID {id} not found.");
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
                return StatusCode(500, "An error occurred while updating the basketball team: " + ex.Message);
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
                    return NotFound($"Basketball team with ID {id} not found.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Basketball team not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the basketball team: " + ex.Message);
            }
        }
    }
}