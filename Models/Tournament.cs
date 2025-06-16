using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LeagueId { get; set; }

    public DateTime StartDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<BasketballGame> BasketballGames { get; set; } = new List<BasketballGame>();

    public virtual League League { get; set; } = null!;

    public virtual ICollection<TeamTournament> TeamTournaments { get; set; } = new List<TeamTournament>();
}
