using SportsLeagueApi.Dtos.Core.TeamDtos;
using SportsLeagueApi.Models;

namespace SportsLeagueApi.Dtos.Core.TournamentDtos
{
    public class TournamentResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int LeagueId { get; set; }
        public DateTime StartDate { get; set; }
        public required string Status { get; set; }
        public List<TeamResponseDto> Teams { get; set; } = new List<TeamResponseDto>();
    }
}