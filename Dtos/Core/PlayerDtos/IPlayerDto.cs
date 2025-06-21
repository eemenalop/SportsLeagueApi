namespace SportsLeagueApi.Dtos.Core.PlayerDtos
{
    public interface IPlayerDto
    {
        string Name { get; set; }

        string LastName { get; set; }

        string DocumentId { get; set; }

        string? Position { get; set; }

        int LeagueId { get; set; }
    }
}