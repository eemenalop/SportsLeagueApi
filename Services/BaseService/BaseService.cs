using Microsoft.EntityFrameworkCore;
using SportsLeagueApi.Data;

namespace SportsLeagueApi.Services.BaseService
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
        public Task<T> Delete(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
