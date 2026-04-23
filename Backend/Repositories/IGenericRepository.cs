using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoulderingGymAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Add(T entity);

        Task<T?> GetById(int id);

        Task<bool> Delete(int id);
    }
}