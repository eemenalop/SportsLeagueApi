using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Data;
using SportsLeagueApi.Dtos.SportDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.SportService;

namespace SportsLeagueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SportController : BaseController<Sport>
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService) : base(sportService)
        {
            _sportService = sportService;
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
                return CreatedAtAction(nameof(GetById), new { id = newSport.Id }, newSport);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Server error while creating sport");
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
            catch (Exception)
            {
                return BadRequest("Server error while updating sport");
            }
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteSport(int id)
        {

            try
            {
                var sport = await _sportService.GetById(id);
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
            catch(Exception)
            {
                return BadRequest("Server error while deleting sport");
            }
        }
    }
}