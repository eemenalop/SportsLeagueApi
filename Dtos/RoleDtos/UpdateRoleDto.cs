namespace SportsLeagueApi.Dtos.RoleDtos
{
    public class UpdateRoleDto
    {
        public string Name { get; set; } = null!;
        public string? Permissions { get; set; }
        public string? Description { get; set; }
    }
}
    