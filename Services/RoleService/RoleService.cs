using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.RoleDtos;
using SportsLeagueApi.Data;
using Microsoft.EntityFrameworkCore;

namespace SportsLeagueApi.Services.RoleService
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly AppDbContext _roleContext;

        public RoleService(AppDbContext context) : base(context)
        {
            _roleContext = context;
        }

        public async Task<Role> CreateRole(CreateRoleDto roleDto)
        {
            var newRole = new Role
            {
                Name = roleDto.Name,
                Permissions = roleDto.Permissions,
                Description = roleDto.Description
            };
            await _roleContext.Roles.AddAsync(newRole);
            await _roleContext.SaveChangesAsync();
            return newRole;
        }

        public async Task<Role> UpdateRole(int id, UpdateRoleDto roleDto)
        {
            var existingRole = await _roleContext.Roles.FindAsync(id);
            if (existingRole == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            existingRole.Name = roleDto.Name;
            existingRole.Description = roleDto.Description;
            existingRole.Permissions = roleDto.Permissions;
            await _roleContext.SaveChangesAsync();
            return existingRole;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var role = await _roleContext.Roles.FindAsync(id);
            if (role == null) {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            _roleContext.Roles.Remove(role);
            await _roleContext.SaveChangesAsync();
            return true;
        }
    }
}