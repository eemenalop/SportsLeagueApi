namespace SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos
{
    public interface UpdateBasketballTeamDto : IBasketballTeamDto
    {
        public string Name { get; set; }
    }
}