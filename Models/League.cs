using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class League
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SportId { get; set; }

    public int AdminId { get; set; }

    public virtual Account Admin { get; set; } = null!;

    public virtual Sport Sport { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();

    public virtual ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
}
