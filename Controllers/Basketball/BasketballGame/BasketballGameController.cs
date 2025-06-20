using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Services.Basketball.BasketballGameService;
using sportsLeagueApi.Dtos.Basketball.BasketballGameDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballGameDtos;

namespace SportsLeagueApi.Controllers.Basketball.BasketballGameController
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketballGameController : BaseController<BasketballGame>
    {
        private readonly IBasketballGameService _basketballGameService;

        public BasketballGameController(IBasketballGameService basketballGameService) : base(basketballGameService)
        {
            _basketballGameService = basketballGameService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketballGame([FromBody] CreateBasketballGameDto gameDto)
        {
            try
            {
                if (gameDto == null)
                    throw new ArgumentException("Game data cannot be null.");

                var game = await _basketballGameService.CreateBasketballGame(gameDto);
                return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the basketball game: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasketballGame(int id, [FromBody] UpdateBasketballGameDto gameDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid game ID.");
                if (gameDto == null)
                    throw new ArgumentException("Game data cannot be null.");
                var updatedGame = await _basketballGameService.UpdateBasketballGame(id, gameDto);
                if (updatedGame == null)
                    return NotFound($"Basketball game with ID {id} not found.");

                return Ok(updatedGame);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Basketball game not found: {ex.Message}");
            }
            catch (System.Exception)
            {
                return StatusCode(500, "An error occurred while updating the basketball game.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketballGame(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid game ID.");

                var isDeleted = await _basketballGameService.DeleteBasketballGame(id);
                if (!isDeleted)
                    return NotFound($"Basketball game with ID {id} not found.");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Basketball game not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the basketball game: " + ex.Message);
            }
        }
        
    }
}