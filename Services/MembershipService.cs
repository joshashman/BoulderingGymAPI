using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class MembershipService
    {
        private readonly GymDbContext _context;

        public MembershipService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<Membership>> GetAllMemberships()
        {
            return await _context.Memberships.ToListAsync();
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            _context.Memberships.Add(membership);

            await _context.SaveChangesAsync();

            return membership;
        }

        public async Task<bool> DeleteMembership(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
                return false;

            _context.Memberships.Remove(membership);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}