using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLeagueApi.Services.RoleService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.RoleDtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SportsLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Role>
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) : base(roleService)
        {

            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleDto)
        {
            try
            {
                var newRole = await _roleService.CreateRole(roleDto);
                if (newRole == null)
                {
                    return BadRequest("Error creating role");
                }
                return CreatedAtAction(nameof(GetById), new { id = newRole.Id }, newRole);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while creating role: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto roleDto)
        {
            try
            {
                var updatedRole = await _roleService.UpdateRole(id, roleDto);
                if (updatedRole == null)
                {
                    return NotFound($"Role with ID {id} not found.");
                }
                return Ok(updatedRole);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Server error while updating role: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var role = await _roleService.GetById(id);
                if (role == null)
                {
                    return NotFound($"Role with ID {id} not found.");
                }
                await _roleService.DeleteRole(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"Server error while deleting role: {ex.Message}");
            }
        }
    }
}