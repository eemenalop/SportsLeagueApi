namespace SportsLeagueApi.Dtos.Core.PlayerDtos
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; }= null!;
        public string DocumentId { get; set; }= null!;
        public DateTime BirthDate { get; set; }
        public string? Position { get; set; }
        public string TeamName { get; set; }= null!;
    }
}