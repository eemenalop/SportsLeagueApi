namespace SportsLeagueApi.Dtos.Basketball.TournamentDtos
{
    public interface CreateTournamentDto : ITournamentDto
    {
        public string Name { get; set; }
        public int LeagueId { get; set; }
    }
}