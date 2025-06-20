using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class PlayerTeam
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int TeamId { get; set; }

    public int TournamentId { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
