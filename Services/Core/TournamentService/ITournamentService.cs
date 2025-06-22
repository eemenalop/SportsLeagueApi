using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.TournamentDtos;

namespace SportsLeagueApi.Services.Core.TournamentService
{
    public interface ITournamentService
    {   Task<IEnumerable<TournamentResponseDto>> GetAllTournaments();
        Task<TournamentResponseDto> GetTournamentById(int id);
        Task<TournamentResponseDto> CreateTournament(CreateTournamentDto tournamentDto);
        Task<TournamentResponseDto> UpdateTournament(int id, UpdateTournamentDto tournamentDto);
        Task<bool> DeleteTournament(int id);
    }
}