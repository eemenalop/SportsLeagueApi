using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Core.TournamentDtos;
using SportsLeagueApi.Data;
using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Dtos.Core.TeamDtos;
using Newtonsoft.Json;

namespace SportsLeagueApi.Services.Core.TournamentService
{
    public class TournamentService : ITournamentService
    {
        private readonly AppDbContext _tournamentContext;
        public TournamentService(AppDbContext context)
        {
            _tournamentContext = context;
        }
        private void ValitateTournamentDto(ITournamentDto tournamentDto)
        {
            if (tournamentDto == null)
                throw new ArgumentNullException(nameof(tournamentDto), "Tournament DTO cannot be null.");
            if (string.IsNullOrWhiteSpace(tournamentDto.Name))
                throw new ArgumentException("Tournament name cannot be empty.");

            if (tournamentDto.Name.Length > 100)
                throw new ArgumentException("Tournament name cannot exceed 100 characters.");

            if (!char.IsUpper(tournamentDto.Name[0]))
                throw new ArgumentException("Tournament name must start with an uppercase letter.");

            if (string.IsNullOrWhiteSpace(tournamentDto.LeagueId.ToString()))
                throw new ArgumentException("League ID cannot be empty.");

            if (tournamentDto.LeagueId <= 0)
                throw new ArgumentException("League ID must be a positive integer.");

            if (tournamentDto.StartDate == default)
                throw new ArgumentException("Start date is required.");

            if (string.IsNullOrWhiteSpace(tournamentDto.Status))
                throw new ArgumentException("Status cannot be empty.");

            if (tournamentDto.Status != "Active" && tournamentDto.Status != "Canceled" && tournamentDto.Status != "Finished")
                throw new ArgumentException("Status must be either 'Active', 'Canceled' or 'Finished'.");
        }
        public async Task<IEnumerable<TournamentResponseDto>> GetAllTournaments()
        {
            if (_tournamentContext.Tournaments == null)
                throw new KeyNotFoundException("No tournaments found in the database.");
            var tournaments = await _tournamentContext.Tournaments.ToListAsync();
            var tournamentsResponse = tournaments.Select(t => new TournamentResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                LeagueId = t.LeagueId,
                StartDate = t.StartDate,
                Status = t.Status
            }).ToList();
            var json = JsonConvert.SerializeObject(_tournamentContext.Teams, Formatting.Indented);
            Console.WriteLine(json);
            return tournamentsResponse;
        }
        public async Task<TournamentResponseDto> GetTournamentById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid tournament ID.");
            var tournament = await _tournamentContext.Tournaments.FindAsync(id);
            if (tournament == null) throw new KeyNotFoundException($"Tournament with ID {id} not found.");
            var tournamentResponse = new TournamentResponseDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                LeagueId = tournament.LeagueId,
                StartDate = tournament.StartDate,
                Status = tournament.Status,
                Teams = tournament.TeamTournaments
                    .Where(tt => tt.TournamentId == tournament.Id)
                    .Select(tt => new TeamResponseDto
                    {
                        Id = tt.Team.Id,
                        Name = tt.Team.Name,
                        CreatedAt = tt.Team.CreatedAt,
                        LogoUrl = tt.Team.LogoUrl
                    }).ToList()
            };
            return tournamentResponse;
        }
        public async Task<TournamentResponseDto> CreateTournament(CreateTournamentDto tournamentDto)
        {
            ValitateTournamentDto(tournamentDto);
            var tournament = new Tournament
            {
                Name = tournamentDto.Name,
                LeagueId = tournamentDto.LeagueId,
                StartDate = tournamentDto.StartDate,
                Status = tournamentDto.Status,
                TeamTournaments = new List<TeamTournament>()
            };
            var existingLeageue = await _tournamentContext.Leagues.FindAsync(tournamentDto.LeagueId);
            if (existingLeageue == null)
                throw new KeyNotFoundException($"League with ID {tournamentDto.LeagueId} not found.");


            await _tournamentContext.Tournaments.AddAsync(tournament);
            await _tournamentContext.SaveChangesAsync();
            var tournamentResponse = new TournamentResponseDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                LeagueId = tournament.LeagueId,
                StartDate = tournament.StartDate,
                Status = tournament.Status,
                Teams = tournament.TeamTournaments
                    .Where(tt => tt.TournamentId == tournament.Id)
                    .Select(tt => new TeamResponseDto
                    {
                        Id = tt.Team.Id,
                        Name = tt.Team.Name,
                        CreatedAt = tt.Team.CreatedAt,
                        LogoUrl = tt.Team.LogoUrl
                    }).ToList()
            };
            return tournamentResponse;
        }

        public async Task<TournamentResponseDto> UpdateTournament(int id, UpdateTournamentDto tournamentDto)
        {
            ValitateTournamentDto(tournamentDto);
            if (id <= 0) throw new ArgumentException("Invalid tournament ID.");
            var tournament = await _tournamentContext.Tournaments.FindAsync(id);
            if (tournament == null) throw new KeyNotFoundException($"Tournament with ID {id} not found.");

            tournament.Name = tournamentDto.Name;
            tournament.LeagueId = tournamentDto.LeagueId;
            tournament.StartDate = tournamentDto.StartDate;
            tournament.Status = tournamentDto.Status;

            var existingLeague = await _tournamentContext.Leagues.FindAsync(tournamentDto.LeagueId);
            if (existingLeague == null)
                throw new KeyNotFoundException($"League with ID {tournamentDto.LeagueId} not found.");

            _tournamentContext.Tournaments.Update(tournament);
            await _tournamentContext.SaveChangesAsync();
            var tournamentResponse = new TournamentResponseDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                LeagueId = tournament.LeagueId,
                StartDate = tournament.StartDate,
                Status = tournament.Status,
                Teams = tournament.TeamTournaments
                    .Where(tt => tt.TournamentId == tournament.Id)
                    .Select(tt => new TeamResponseDto
                    {
                        Id = tt.Team.Id,
                        Name = tt.Team.Name,
                        CreatedAt = tt.Team.CreatedAt,
                        LogoUrl = tt.Team.LogoUrl
                    }).ToList()
            };

            return tournamentResponse;
        }

        public async Task<bool> DeleteTournament(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid tournament ID.");
            var tournament = await _tournamentContext.Tournaments.FindAsync(id);
            if (tournament == null)
                throw new KeyNotFoundException($"Tournament with ID {id} not found.");

            _tournamentContext.Tournaments.Remove(tournament);
            await _tournamentContext.SaveChangesAsync();

            return true;
        }
    }
}