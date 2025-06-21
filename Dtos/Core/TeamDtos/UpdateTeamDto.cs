namespace SportsLeagueApi.Dtos.Core.TeamDtos
{
    public class UpdateTeamDto : ITeamDto
    {
        public string Name { get; set; }= null!;
        public DateTime? CreatedAt { get; set; }
        public string? LogoUrl { get; set; }
        public int TournamentId { get; set; }
    }
}