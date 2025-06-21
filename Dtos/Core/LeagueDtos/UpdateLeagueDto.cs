namespace SportsLeagueApi.Dtos.Core.LeagueDtos
{
    public class UpdateLeagueDto : ILeagueDto
    {
        public string Name { get; set; } = null!;
        public int SportId { get; set; }
        public int AdminId { get; set; }
    }    
}