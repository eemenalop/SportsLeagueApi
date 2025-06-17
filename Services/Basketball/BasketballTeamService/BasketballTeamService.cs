using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Services.Basketball.BasketballTeamService;
using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos;
using SportsLeagueApi.Data;

namespace SportsLeagueApi.Services.Basketball.BasketballTeamService
{
    public class BasketballTeamService : BaseService<BasketballTeam>, IBasketballTeamService
    {
        private readonly AppDbContext _teamContext;
        public BasketballTeamService(AppDbContext teamContext) : base(teamContext)
        {
            _teamContext = teamContext;
        }

        private void ValidateBasketballTeam(IBasketballTeamDto teamDto)
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

        public async Task<BasketballTeam?> CreateBasketballTeam(CreateBasketballTeamDto teamDto)
        {
            ValidateBasketballTeam(teamDto);
            var team = new BasketballTeam
            {
                Name = teamDto.Name,
            };
            await _teamContext.BasketballTeams.AddAsync(team);
            await _teamContext.SaveChangesAsync();
            return team;
        }
        public async Task<BasketballTeam?> UpdateBasketballTeam(int id, UpdateBasketballTeamDto teamDto)
        {
            ValidateBasketballTeam(teamDto);
            if (id <= 0)
            {
                throw new ArgumentException("Invalid team ID.");
            }
            var team = await _teamContext.BasketballTeams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Basketball team with ID {id} not found.");
            }
            team.Name = teamDto.Name;
            _teamContext.BasketballTeams.Update(team);
            await _teamContext.SaveChangesAsync();
            return team;
        }
        public async Task<bool> DeleteBasketballTeam(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid team ID.");
            }
            var team = await _teamContext.BasketballTeams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Basketball team with ID {id} not found.");
            }
            _teamContext.BasketballTeams.Remove(team);
            await _teamContext.SaveChangesAsync();
            return true;
        }
    }
}