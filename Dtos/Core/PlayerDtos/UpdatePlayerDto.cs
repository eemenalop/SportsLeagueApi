namespace SportsLeagueApi.Dtos.Core.PlayerDtos
{
    public class UpdatePlayerDto : IPlayerDto
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DocumentId { get; set; } = null!;

        public string? Position { get; set; }

        public int LeagueId { get; set; }

    }
}