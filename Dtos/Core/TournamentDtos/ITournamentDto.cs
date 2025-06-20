namespace SportsLeagueApi.Dtos.Core.TournamentDtos
{
    public interface ITournamentDto
    {
        string Name { get; set; }
        int LeagueId { get; set; }
        DateTime StartDate { get; set; }
        string Status { get; set; }
    }
}