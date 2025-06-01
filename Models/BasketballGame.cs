using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class BasketballGame
{
    public int Id { get; set; }

    public int TeamA { get; set; }

    public int TeamB { get; set; }

    public int? ScoreTeamA { get; set; }

    public int? ScoreTeamB { get; set; }

    public int TournamentId { get; set; }

    public string GameType { get; set; } = null!;

    public string State { get; set; } = null!;

    public virtual ICollection<PlayerBasketballStat> PlayerBasketballStats { get; set; } = new List<PlayerBasketballStat>();

    public virtual BasketballTeam TeamANavigation { get; set; } = null!;

    public virtual BasketballTeam TeamBNavigation { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
