using BoulderingGymAPI.Models;
using BoulderingGymAPI.Repositories;

namespace BoulderingGymAPI.Services
{
    public class SessionService
    {
        private readonly IGenericRepository<Session> _repository;

        public SessionService(IGenericRepository<Session> repository)
        {
            _repository = repository;
        }

        public async Task<List<Session>> GetAllSessions()
        {
            return await _repository.GetAll();
        }

        public async Task<Session> CreateSession(Session session)
        {
            return await _repository.Add(session);
        }

        public async Task<bool> DeleteSession(int id)
        {
            return await _repository.Delete(id);
        }
    }
}