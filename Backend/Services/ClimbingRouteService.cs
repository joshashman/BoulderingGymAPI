using BoulderingGymAPI.Models;
using BoulderingGymAPI.Repositories;

namespace BoulderingGymAPI.Services
{
    public class ClimbingRouteService
    {
        private readonly IGenericRepository<ClimbingRoute> _repository;

        public ClimbingRouteService(IGenericRepository<ClimbingRoute> repository)
        {
            _repository = repository;
        }

        public async Task<List<ClimbingRoute>> GetAllRoutes()
        {
            return await _repository.GetAll();;
        }

        public async Task<ClimbingRoute> CreateRoute(ClimbingRoute route)
        {
            return await _repository.Add(route);
        }

        public async Task<bool> DeleteRoute(int id)
        {
            return await _repository.Delete(id);
        }
    }
}