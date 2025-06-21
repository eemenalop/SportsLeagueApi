namespace SportsLeagueApi.Dtos.Core.SportDtos
{
    public class CreateSportDto : ISportDto
    {
        public string Name { get; set; } = null!;
    }
}