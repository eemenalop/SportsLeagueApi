namespace SportsLeagueApi.Services.BaseService
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(int id, T entity);
    }
}
