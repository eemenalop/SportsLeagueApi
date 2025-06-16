using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Services.BaseService;

namespace SportsLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IBaseService<T> _service;

        public BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _service.GetAll();
                return Ok(entities);
            }
            catch(ArgumentException ex)
            {
                return BadRequest("Error getting data: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _service.GetById(id);
                if (entity == null)
                {
                    return NotFound();
                }
            return Ok(entity);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Error getting data: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
