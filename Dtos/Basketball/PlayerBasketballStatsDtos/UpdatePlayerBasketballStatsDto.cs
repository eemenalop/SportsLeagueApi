namespace SportsLeagueApi.Dtos.Basketball.PlayerBasketballStatsDtos
{
    public class UpdatePlayerBasketballStatsDto : IPlayerBasketballStatsDto
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int? Points { get; set; }
        public int? Rebounds { get; set; }
        public int? Assists { get; set; }
        public int? Steals { get; set; }
        public int? Blocks { get; set; }
        public int? Turnovers { get; set; }
        public int? Fga { get; set; }
        public int? Fgm { get; set; }
        public int? Threepta { get; set; }
        public int? Threeptm { get; set; }
        public int? Fta { get; set; }
        public int? Ftm { get; set; }
        public int? Fouls { get; set; }
    }
}