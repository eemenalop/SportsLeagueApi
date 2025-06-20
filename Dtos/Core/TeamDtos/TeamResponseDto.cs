namespace SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos
{
    public class TeamResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? LogoUrl { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; } = null!;
    }
}