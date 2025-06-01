using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class UserLeague
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int LeagueId { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
