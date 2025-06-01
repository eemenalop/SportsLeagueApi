using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string? Position { get; set; }

    public virtual ICollection<PlayerBasketballStat> PlayerBasketballStats { get; set; } = new List<PlayerBasketballStat>();

    public virtual ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
}
