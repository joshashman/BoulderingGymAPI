using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;

namespace BoulderingGymAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GymDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}