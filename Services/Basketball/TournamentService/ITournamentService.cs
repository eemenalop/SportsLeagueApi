using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;
using SportsLeagueApi.Dtos.Basketball.TournamentDtos;

namespace SportsLeagueApi.Services.Basketball.TournamentService
{
    public interface ITournamentService : IBaseService<Tournament>
    {
        Task<Tournament> CreateTournament(CreateTournamentDto tournamentDto);
        Task<Tournament> UpdateTournament(int id, UpdateTournamentDto tournamentDto);
        Task<bool> DeleteTournament(int id);
    }
}