using System;
using System.Collections.Generic;

namespace SportsLeagueApi.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? DocumentId { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<League> Leagues { get; set; } = new List<League>();

    public virtual Role Role { get; set; } = null!;
}
