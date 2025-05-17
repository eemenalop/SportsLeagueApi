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
        public async Task<T> Update(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null) {
                throw new Exception("Model does not hace property Id");
            }

            var id = (int)idProperty.GetValue(entity);
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null) { 
                return null;
            }

            _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
        public async Task<T> Delete(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                throw new Exception("Model does not hace property Id");
            }

            var id = (int)idProperty.GetValue(entity);
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
            {
                return null;
            }

            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
