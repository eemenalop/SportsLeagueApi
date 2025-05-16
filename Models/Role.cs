﻿using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Permissions { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
