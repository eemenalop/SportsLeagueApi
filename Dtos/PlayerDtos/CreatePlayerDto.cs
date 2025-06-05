namespace SportsLeagueApi.Dtos.PlayerDtos
{
    public class CreatePlayerDto
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DocumentId { get; set; } = null!;

        public string? Position { get; set; }

    }
}