using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Models;

namespace SportsLeagueApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<BasketballGame> BasketballGames { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerBasketballStat> PlayerBasketballStats { get; set; }

    public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamTournament> TeamTournaments { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<UserLeague> UserLeagues { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account", "core");

            entity.HasIndex(e => e.Email, "account_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(100)
                .HasColumnName("document_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_role");
        });

        modelBuilder.Entity<BasketballGame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basketball_game_pkey");

            entity.ToTable("basketball_game", "basketball");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.GameType)
                .HasMaxLength(50)
                .HasColumnName("game_type");
            entity.Property(e => e.ScoreTeamA).HasColumnName("score_team_a");
            entity.Property(e => e.ScoreTeamB).HasColumnName("score_team_b");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.TeamA).HasColumnName("team_a");
            entity.Property(e => e.TeamB).HasColumnName("team_b");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.TeamANavigation).WithMany(p => p.BasketballGameTeamANavigations)
                .HasForeignKey(d => d.TeamA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_team_a");

            entity.HasOne(d => d.TeamBNavigation).WithMany(p => p.BasketballGameTeamBNavigations)
                .HasForeignKey(d => d.TeamB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_team_b");

            entity.HasOne(d => d.Tournament).WithMany(p => p.BasketballGames)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_tournament");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("league_pkey");

            entity.ToTable("league", "core");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SportId).HasColumnName("sport_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Leagues)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_league_admin");

            entity.HasOne(d => d.Sport).WithMany(p => p.Leagues)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_league_sport");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_pkey");

            entity.ToTable("player", "core");

            entity.HasIndex(e => e.DocumentId, "player_document_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(100)
                .HasColumnName("document_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
        });

        modelBuilder.Entity<PlayerBasketballStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_basketball_stats_pkey");

            entity.ToTable("player_basketball_stats", "basketball");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Assists).HasColumnName("assists");
            entity.Property(e => e.Blocks).HasColumnName("blocks");
            entity.Property(e => e.Fga).HasColumnName("fga");
            entity.Property(e => e.Fgm).HasColumnName("fgm");
            entity.Property(e => e.Fouls).HasColumnName("fouls");
            entity.Property(e => e.Fta).HasColumnName("fta");
            entity.Property(e => e.Ftm).HasColumnName("ftm");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Rebounds).HasColumnName("rebounds");
            entity.Property(e => e.Steals).HasColumnName("steals");
            entity.Property(e => e.Threepta).HasColumnName("threepta");
            entity.Property(e => e.Threeptm).HasColumnName("threeptm");
            entity.Property(e => e.Turnovers).HasColumnName("turnovers");

            entity.HasOne(d => d.Game).WithMany(p => p.PlayerBasketballStats)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_game");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerBasketballStats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_player");
        });

        modelBuilder.Entity<PlayerTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_team_pkey");

            entity.ToTable("player_team", "core");

            entity.HasIndex(e => new { e.PlayerId, e.TeamId, e.TournamentId }, "player_team_player_id_team_id_tournament_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("player_team_player_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("player_team_team_id_fkey");

            entity.HasOne(d => d.Tournament).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TournamentId)
                .HasConstraintName("player_team_tournament_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "core");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Permissions).HasColumnName("permissions");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sport_pkey");

            entity.ToTable("sport", "core");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basketball_team_pkey");

            entity.ToTable("team", "core");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.LogoUrl).HasColumnName("logo_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TeamTournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_tournament_pkey");

            entity.ToTable("team_tournament", "core");

            entity.HasIndex(e => new { e.TeamId, e.TournamentId }, "team_tournament_team_id_tournament_id_key").IsUnique();

            entity.HasIndex(e => new { e.TeamId, e.TournamentId }, "uq_team_tournament").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("create_at");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamTournaments)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_team_tournament_team");

            entity.HasOne(d => d.Tournament).WithMany(p => p.TeamTournaments)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_team_tournament_tournament");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tournament_pkey");

            entity.ToTable("tournament", "core");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.LeagueId).HasColumnName("league_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.League).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.LeagueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tournament_league");
        });

        modelBuilder.Entity<UserLeague>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_league_pkey");

            entity.ToTable("user_league", "core");

            entity.HasIndex(e => new { e.PlayerId, e.LeagueId }, "uq_user_league").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.LeagueId).HasColumnName("league_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.League).WithMany(p => p.UserLeagues)
                .HasForeignKey(d => d.LeagueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_league_league");

            entity.HasOne(d => d.Player).WithMany(p => p.UserLeagues)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_league_player");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
