using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.TournamentService;
using SportsLeagueApi.Dtos.Core.TournamentDtos;

namespace SportsLeagueApi.Controllers.Core.TournamentController
{

    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            try
            {
                var tournaments = await _tournamentService.GetAllTournaments();
                return Ok(tournaments);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"No tournaments found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while retrieving tournaments: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid tournament ID.");
                var tournament = await _tournamentService.GetTournamentById(id);
                return Ok(tournament);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Tournament with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while retrieving the tournament: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto tournamentDto)
        {
            try
            {
                if (tournamentDto == null)
                    throw new ArgumentException("Tournament data cannot be null.");

                var tournament = await _tournamentService.CreateTournament(tournamentDto);
                return CreatedAtAction(nameof(GetById), new { id = tournament.Id }, tournament);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"League not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while creating the tournament: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] UpdateTournamentDto tournamentDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid tournament ID.");
                if (tournamentDto == null)
                    throw new ArgumentException("Tournament data cannot be null.");

                var updatedTournament = await _tournamentService.UpdateTournament(id, tournamentDto);
                if (updatedTournament == null)
                {
                    return NotFound($"Tournament with ID {id} not found.");
                }
                return Ok(updatedTournament);
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
                return StatusCode(500, "Server error while updating the tournament: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid tournament ID.");
                var deleted = await _tournamentService.DeleteTournament(id);
                if (!deleted)
                {
                    return NotFound($"Tournament with ID {id} not found.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Tournament not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error while deleting the tournament: " + ex.Message);
            }
        }
    }
}