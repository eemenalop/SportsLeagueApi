using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;
using SportsLeagueApi.Services.Core.TeamService;
using SportsLeagueApi.Data;
using Microsoft.EntityFrameworkCore;

namespace SportsLeagueApi.Services.Core.TeamService
{
    public class TeamService : BaseService<Team>, ITeamService
    {
        private readonly AppDbContext _teamContext;
        public TeamService(AppDbContext teamContext) : base(teamContext)
        {
            _teamContext = teamContext;
        }

        private void ValidateBasketballTeam(ITeamDto teamDto)
        {
            if (teamDto == null)
            {
                throw new ArgumentNullException(nameof(teamDto), "Team DTO cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(teamDto.Name))
            {
                throw new ArgumentException("Team name cannot be empty.");
            }
            if (!char.IsUpper(teamDto.Name[0]))
            {
                throw new ArgumentException("Team name must start with an uppercase letter.");
            }
            if (teamDto.Name.Length < 3 || teamDto.Name.Length > 50)
            {
                throw new ArgumentException("Team name must be between 3 and 50 characters long.");
            }
        }

        public async Task<TeamResponseDto> CreateTeam(CreateTeamDto teamDto)
        {
            ValidateBasketballTeam(teamDto);
            var existingTournament = await _teamContext.Tournaments.FindAsync(teamDto.TournamentId);
            if (existingTournament == null)
            {
                throw new KeyNotFoundException($"Tournament with ID {teamDto.TournamentId} not found.");
            }
            var team = new Team
            {
                Name = teamDto.Name,
            };
            await _teamContext.Teams.AddAsync(team);
            await _teamContext.SaveChangesAsync();
            var teamTournament = new TeamTournament
            {
                TeamId = team.Id,
                TournamentId = teamDto.TournamentId
            };
            await _teamContext.TeamTournaments.AddAsync(teamTournament);
            await _teamContext.SaveChangesAsync();
            var responseDto = new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name,
                TournamentId = teamDto.TournamentId,
                TournamentName = existingTournament.Name
            };
            return responseDto;
        }
        public async Task<TeamResponseDto> UpdateTeam(int id, UpdateTeamDto teamDto)
        {
            ValidateBasketballTeam(teamDto);
            if (id <= 0)
            {
                throw new ArgumentException("Invalid team ID.");
            }
            var team = await _teamContext.Teams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Team with ID {id} not found.");
            }
            team.Name = teamDto.Name;
            var teamTournament = await _teamContext.TeamTournaments.FirstOrDefaultAsync(tt => tt.TeamId == id);
            if (teamTournament == null)
            {
                throw new KeyNotFoundException($"TeamTournament for team with ID {id} not found.");
            }
            teamTournament.TournamentId = teamDto.TournamentId;
            var existingTournament = await _teamContext.Tournaments.FindAsync(teamDto.TournamentId);
            if (existingTournament == null)
            {
                throw new KeyNotFoundException($"Tournament with ID {teamDto.TournamentId} not found.");
            }
            
            _teamContext.Teams.Update(team);
            await _teamContext.SaveChangesAsync();
            var responseDto = new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name,
                TournamentId = teamTournament.TournamentId,
                TournamentName = existingTournament.Name
            };
            return responseDto;
        }
        public async Task<bool> DeleteTeam(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid team ID.");
            }
            var team = await _teamContext.Teams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Team with ID {id} not found.");
            }
            var teamTournaments = await _teamContext.TeamTournaments.FirstOrDefaultAsync(tt => tt.TeamId == id);
            if (teamTournaments != null)
            {
                _teamContext.TeamTournaments.Remove(teamTournaments);
            }
            _teamContext.Teams.Remove(team);
            await _teamContext.SaveChangesAsync();
            return true;
        }
    }
}