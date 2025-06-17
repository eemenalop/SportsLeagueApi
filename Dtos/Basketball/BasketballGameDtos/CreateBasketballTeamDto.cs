using SportsLeagueApi.Dtos.Basketball.BasketballGameDtos;

namespace sportsLeagueApi.Dtos.Basketball.BasketballGameDtos
{
    public class CreateBasketballGameDto : IBasketballGameDto
    {
        public int TeamA { get; set; }
        public int TeamB { get; set; }
        public int ScoreTeamA { get; set; }
        public int ScoreTeamB { get; set; }
        public int TournamentId { get; set; }
        public string GameType { get; set; } = null!;
        public string State { get; set; } = null!;
    }
}