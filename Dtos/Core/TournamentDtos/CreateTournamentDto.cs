namespace SportsLeagueApi.Dtos.Core.TournamentDtos
{
    public class CreateTournamentDto : ITournamentDto
    {
        public string Name { get; set; } = null!;
        public int LeagueId { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } = null!;
    }
}