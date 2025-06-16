namespace SportsLeagueApi.Dtos.RoleDtos
{
    public class UpdateRoleDto : IRoleDto
    {
        public string Name { get; set; } = null!;
        public string Permissions { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
    