namespace SportsLeagueApi.Dtos.Core.RoleDtos
{
    public class CreateRoleDto : IRoleDto
    {
        public string Name { get; set; } = null!;
        public string Permissions { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
    