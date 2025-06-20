using SportsLeagueApi.Dtos.SportDtos;
using SportsLeagueApi.Models;
using SportsLeagueApi.Services.BaseService;

namespace SportsLeagueApi.Services.Core.SportService
{
    public interface ISportService : IBaseService<Sport>
    {
        Task<Sport> CreateSport(CreateSportDto sportDto);
        Task<Sport> UpdateSport(int id, UpdateSportDto sportDto);
        Task<bool> DeleteSport(int id);
    }
}