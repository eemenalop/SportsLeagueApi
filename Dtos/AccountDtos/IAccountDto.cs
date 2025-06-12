namespace SportsLeagueApi.Dtos.AccountDtos
{
    public interface IAccountDto
    {
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        int RoleId { get; set; }
        string DocumentId { get; set; }
        string? Phone { get; set; }
        DateTime? DateOfBirth { get; set; }
    }
}