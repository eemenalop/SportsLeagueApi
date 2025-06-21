using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.RoleDtos;
using SportsLeagueApi.Data;
using Microsoft.EntityFrameworkCore;

namespace SportsLeagueApi.Services.Core.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _roleContext;

        public RoleService(AppDbContext context)
        {
            _roleContext = context;
        }
        private void ValidateRoleDto(IRoleDto roleDto)
        {
            if (roleDto == null)
            {
                throw new ArgumentNullException(nameof(roleDto), "Role data cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(roleDto.Name))
            {
                throw new ArgumentException("Role name cannot be empty.");
            }
            if (roleDto.Name.Length < 3)
            {
                throw new ArgumentException("Role name must be at least 3 characters long.");
            }
            if (roleDto.Name.Length > 50)
            {
                throw new ArgumentException("Role name cannot exceed 50 characters.");
            }
            if (!char.IsUpper(roleDto.Name[0]))
            {
                throw new ArgumentException("Role name must start with an uppercase letter.");
            }
            if (roleDto.Permissions == null || !roleDto.Permissions.Any())
            {
                throw new ArgumentException("Role must have at least one permission.");
            }
            if (roleDto.Description != null && roleDto.Description.Length > 500)
            {
                throw new ArgumentException("Role description cannot exceed 500 characters.");
            }
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var roles = await _roleContext.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                throw new KeyNotFoundException("No roles found.");
            }
            return roles;
        }
        public async Task<Role> GetRoleById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid role ID.");
            }
            var role = await _roleContext.Roles.FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            return role;
        }
        public async Task<Role> CreateRole(CreateRoleDto roleDto)
        {
            ValidateRoleDto(roleDto);
            if (await _roleContext.Roles.AnyAsync(r => r.Name == roleDto.Name))
            {
                throw new ArgumentException($"Role with name {roleDto.Name} already exists.");
            }
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid role ID.");
            }
            ValidateRoleDto(roleDto);
            if (await _roleContext.Roles.AnyAsync(r => r.Name == roleDto.Name && r.Id != id))
            {
                throw new ArgumentException($"Role with name {roleDto.Name} already exists.");
            }
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid role ID.");
            }
            if (await _roleContext.Roles.AnyAsync(r => r.Id == id && r.Name == "Admin"))
            {
                throw new ArgumentException("Cannot delete the Admin role.");
            }
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