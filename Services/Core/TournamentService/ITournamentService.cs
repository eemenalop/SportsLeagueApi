using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.TournamentDtos;

namespace SportsLeagueApi.Services.Core.TournamentService
{
    public interface ITournamentService
    {   Task<IEnumerable<Tournament>> GetAllTournaments();
        Task<Tournament> GetTournamentById(int id);
        Task<Tournament> CreateTournament(CreateTournamentDto tournamentDto);
        Task<Tournament> UpdateTournament(int id, UpdateTournamentDto tournamentDto);
        Task<bool> DeleteTournament(int id);
    }
}