namespace SportsLeagueApi.Dtos.Basketball.BasketballGameDtos
{
    public interface IBasketballGameDto
    {
        int TeamA { get; set; }

        int TeamB { get; set; }

        int ScoreTeamA { get; set; }

        int ScoreTeamB { get; set; }

        int TournamentId { get; set; }

        string GameType { get; set; }

        string State { get; set; }
    }
}