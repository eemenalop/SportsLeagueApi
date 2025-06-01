using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class BasketballTeam
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BasketballGame> BasketballGameTeamANavigations { get; set; } = new List<BasketballGame>();

    public virtual ICollection<BasketballGame> BasketballGameTeamBNavigations { get; set; } = new List<BasketballGame>();

    public virtual ICollection<TeamTournament> TeamTournaments { get; set; } = new List<TeamTournament>();
}
