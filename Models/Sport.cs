using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Sport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<League> Leagues { get; set; } = new List<League>();
}
