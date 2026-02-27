using BoulderingGymAPI.Models;
using BoulderingGymAPI.Repositories;

namespace BoulderingGymAPI.Services
{
    public class RouteLikeService
    {
        private readonly IGenericRepository<RouteLike> _repository;

        public RouteLikeService(
            IGenericRepository<RouteLike> repository)
        {
            _repository = repository;
        }

        public async Task<List<RouteLike>> GetAllLikes()
        {
            return await _repository.GetAll();
        }

        public async Task<RouteLike> CreateLike(RouteLike like)
        {
            return await _repository.Add(like);
        }

        public async Task<bool> DeleteLike(int id)
        {
            return await _repository.Delete(id);
        }
    }
}