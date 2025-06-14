namespace SportsLeagueApi.Dtos.Basketball.TournamentDtos
{
    public interface UpdateTournamentDto : ITournamentDto
    {
        public string Name { get; set; }
        public int LeagueId { get; set; }
    }
}