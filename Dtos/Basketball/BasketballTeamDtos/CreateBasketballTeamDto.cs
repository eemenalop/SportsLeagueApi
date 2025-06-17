namespace SportsLeagueApi.Dtos.Basketball.BasketballTeamDtos
{
    public interface CreateBasketballTeamDto : IBasketballTeamDto
    {
        public string Name { get; set; }
    }
}