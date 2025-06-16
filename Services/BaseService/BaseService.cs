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
            if (_dbSet == null)
            {
                throw new ArgumentException("DbSet is not initialized.", nameof(_dbSet));
            }
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} was not found.");
            }
            return entity;
        }
    }
}
