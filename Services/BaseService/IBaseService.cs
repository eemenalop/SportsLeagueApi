namespace SportsLeagueApi.Services.BaseService
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
