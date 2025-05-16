using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class TeamTournament
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int TournamentId { get; set; }

    public virtual BasketballTeam Team { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
