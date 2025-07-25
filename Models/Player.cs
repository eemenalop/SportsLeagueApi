﻿using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Player
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string LastName { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string? Position { get; set; }= null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<PlayerBasketballStat> PlayerBasketballStats { get; set; } = new List<PlayerBasketballStat>();

    public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();

    public virtual ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
}
