using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class RouteAttemptService
    {
        private readonly GymDbContext _context;

        public RouteAttemptService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<RouteAttempt>> GetAllAttempts()
        {
            return await _context.RouteAttempts.ToListAsync();
        }

        public async Task<RouteAttempt> CreateAttempt(RouteAttempt attempt)
        {
            _context.RouteAttempts.Add(attempt);

            await _context.SaveChangesAsync();

            return attempt;
        }

        public async Task<bool> DeleteAttempt(int id)
        {
            var attempt = await _context.RouteAttempts.FindAsync(id);

            if (attempt == null)
                return false;

            _context.RouteAttempts.Remove(attempt);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}