using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Basketball.BasketballTeamService;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;
using SportsLeagueApi.Services.BaseService;

namespace SportsLeagueApi.Controllers.Basketball.BasketballTeamController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketballTeamcontroller : BaseController<BasketballTeam>
    {
        private readonly IBasketballTeamService _basketballTeamService;

        public BasketballTeamcontroller(IBasketballTeamService basketballTeamService) : base(basketballTeamService)
        {
            _basketballTeamService = basketballTeamService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBasketballTeam([FromBody] CreateBasketballTeamDto teamDto)
        {
            try
            {
                if (teamDto == null)
                    throw new ArgumentException("Team data cannot be null.");
                var team = await _basketballTeamService.CreateBasketballTeam(teamDto);
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
        public async Task<IActionResult> UpdateBasketballTeam(int id, [FromBody] UpdateBasketballTeamDto teamDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid team ID.");
                if (teamDto == null)
                    throw new ArgumentException("Team data cannot be null.");

                var updatedTeam = await _basketballTeamService.UpdateBasketballTeam(id, teamDto);
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
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the basketball team: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketballTeam(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid team ID.");

                var result = await _basketballTeamService.DeleteBasketballTeam(id);
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
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the basketball team: " + ex.Message);
            }
        }
    }
}