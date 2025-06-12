namespace SportsLeagueApi.Dtos.AccountDtos
{
    public class CreateAccountDto : IAccountDto
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }

        public string DocumentId { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}
