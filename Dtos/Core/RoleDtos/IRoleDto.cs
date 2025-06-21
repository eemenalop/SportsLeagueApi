namespace SportsLeagueApi.Dtos.Core.RoleDtos
{
    public interface IRoleDto
    {
        string Name { get; set; }
        string Permissions { get; set; }
        string Description { get; set; }
    }
}