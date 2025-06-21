using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.Core.SportDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.Core.SportService;

namespace SportsLeagueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SportController : Controller
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSports()
        {
            try
            {
                var sports = await _sportService.GetAllSports();
                return Ok(sports);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Error retrieving sports: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while retrieving sports: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSportById(int id)
        {
            try
            {
                var sport = await _sportService.GetSportById(id);
                if (sport == null)
                {
                    return NotFound($"Sport with ID {id} not found.");
                }
                return Ok(sport);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Sport with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while retrieving sport: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport([FromBody] CreateSportDto sportDto)
        {
            try
            {
                var newSport = await _sportService.CreateSport(sportDto);
                if (newSport == null)
                {
                    return BadRequest("Error creating Sport, missing or invalid sport data");
                }
                return CreatedAtAction(nameof(GetSportById), new { id = newSport.Id }, newSport);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Sport not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while creating sport: {ex.Message}");
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateSport(int id, [FromBody] UpdateSportDto sportDto)
        {
            try
            {
                var sport = await _sportService.UpdateSport(id, sportDto);
                if (sport == null)
                {
                    return BadRequest($"Sport with ID {id} not found.");
                }
                return Ok(sport);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Sport with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error while updating sport: {ex.Message}");
            }
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteSport(int id)
        {

            try
            {
                var sport = await _sportService.GetSportById(id);
                if (sport == null)
                {
                    return NotFound($"Sport with ID {id} not found.");
                }
                await _sportService.DeleteSport(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while deleting sport: {ex.Message}");
            }
        }
    }
}