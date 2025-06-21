using SportsLeagueApi.Models;
using SportsLeagueApi.Dtos.Core.AccountDtos;

namespace SportsLeagueApi.Dtos.Core.AccountDtos
{
    public class AccountResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string LastName { get; set; } =null!;
        public string Email { get; set; } =null!;
        public string? DocumentId { get; set; }
        public string? Phone { get; set; }=null!;
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}