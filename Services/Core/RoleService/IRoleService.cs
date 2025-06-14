using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.RoleDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Data;

namespace SportsLeagueApi.Services.RoleService
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<Role> CreateRole(CreateRoleDto roleDto);
        Task<Role> UpdateRole(int id, UpdateRoleDto roleDto);
        Task<bool> DeleteRole(int id);

    }
}