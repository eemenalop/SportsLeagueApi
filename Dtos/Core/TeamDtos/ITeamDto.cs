namespace SportsLeagueApi.Dtos.Core.TeamDtos
{
    public interface ITeamDto
    {
        string Name { get; set; }
        DateTime? CreatedAt { get; set; }
        string? LogoUrl { get; set; }
        int TournamentId { get; set; }
    }
}