using SportsLeagueApi.Dtos.Core.RoleDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Data;

namespace SportsLeagueApi.Services.Core.RoleService
{
    public interface IRoleService
    {   Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(CreateRoleDto roleDto);
        Task<Role> UpdateRole(int id, UpdateRoleDto roleDto);
        Task<bool> DeleteRole(int id);

    }
}