using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? LogoUrl { get; set; }

    public virtual ICollection<BasketballGame> BasketballGameTeamANavigations { get; set; } = new List<BasketballGame>();

    public virtual ICollection<BasketballGame> BasketballGameTeamBNavigations { get; set; } = new List<BasketballGame>();

    public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();

    public virtual ICollection<TeamTournament> TeamTournaments { get; set; } = new List<TeamTournament>();
}
