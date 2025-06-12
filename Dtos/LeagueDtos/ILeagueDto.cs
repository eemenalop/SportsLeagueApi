namespace SportsLeagueApi.Dtos.LeagueDtos
{
    public interface ILeagueDto
    {
        string Name { get; set; }
        int SportId { get; set; }
        int AdminId { get; set; }
    }
}