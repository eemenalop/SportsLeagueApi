using SportsLeagueApi.Dtos.Core.SportDtos;
using SportsLeagueApi.Models;

namespace SportsLeagueApi.Services.Core.SportService
{
    public interface ISportService
    {
        Task<IEnumerable<Sport>> GetAllSports();
        Task<Sport> GetSportById(int id);
        Task<Sport> CreateSport(CreateSportDto sportDto);
        Task<Sport> UpdateSport(int id, UpdateSportDto sportDto);
        Task<bool> DeleteSport(int id);
    }
}