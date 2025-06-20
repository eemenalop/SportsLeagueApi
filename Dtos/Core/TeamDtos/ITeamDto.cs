namespace SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos
{
    public interface ITeamDto
    {
        string Name { get; set; }
        DateTime? CreatedAt { get; set; }
        string? LogoUrl { get; set; }
        int TournamentId { get; set; }
    }
}