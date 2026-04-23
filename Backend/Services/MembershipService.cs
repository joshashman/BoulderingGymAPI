using BoulderingGymAPI.Models;
using BoulderingGymAPI.Repositories;

namespace BoulderingGymAPI.Services
{
    public class MembershipService
    {
        private readonly IGenericRepository<Membership> _repository;

        public MembershipService(IGenericRepository<Membership> repository)
        {
            _repository = repository;
        }

        public async Task<List<Membership>> GetAllMemberships()
        {
            return await _repository.GetAll();
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            return await _repository.Add(membership);
        }

        public async Task<bool> DeleteMembership(int id)
        {
            return await _repository.Delete(id);
        }
    }
}