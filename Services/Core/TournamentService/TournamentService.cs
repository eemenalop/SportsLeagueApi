using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Core.TournamentDtos;
using SportsLeagueApi.Data;

namespace SportsLeagueApi.Services.Core.TournamentService
{
    public class TournamentService : BaseService<Tournament>, ITournamentService
    {
        private readonly AppDbContext _tournamentContext;
        public TournamentService(AppDbContext context) : base(context)
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

            if (!(tournamentDto.Status != "Active" || tournamentDto.Status != "Canceled" || tournamentDto.Status != "Finished"))
                throw new ArgumentException("Status must be either 'Active', 'Canceled' or 'Finished'.");
        }
        public async Task<Tournament> CreateTournament(CreateTournamentDto tournamentDto)
        {
            ValitateTournamentDto(tournamentDto);
            var tournament = new Tournament
            {
                Name = tournamentDto.Name,
                LeagueId = tournamentDto.LeagueId,
                StartDate = tournamentDto.StartDate,
                Status = tournamentDto.Status
            };
            var existingLeageue = await _tournamentContext.Leagues.FindAsync(tournamentDto.LeagueId);
            if (existingLeageue == null)
                throw new KeyNotFoundException($"League with ID {tournamentDto.LeagueId} not found.");
            

            await _tournamentContext.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task<Tournament> UpdateTournament(int id, UpdateTournamentDto tournamentDto)
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
            await _context.SaveChangesAsync();

            return tournament;
        }

        public async Task<bool> DeleteTournament(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid tournament ID.");
            var tournament = await _tournamentContext.Tournaments.FindAsync(id);
            if (tournament == null)
                throw new KeyNotFoundException($"Tournament with ID {id} not found.");

            _tournamentContext.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}