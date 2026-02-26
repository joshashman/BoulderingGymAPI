using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class RouteLikeService
    {
        private readonly GymDbContext _context;

        public RouteLikeService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<RouteLike>> GetAllLikes()
        {
            return await _context.RouteLikes.ToListAsync();
        }

        public async Task<RouteLike> CreateLike(RouteLike like)
        {
            _context.RouteLikes.Add(like);

            await _context.SaveChangesAsync();

            return like;
        }

        public async Task<bool> DeleteLike(int id)
        {
            var like = await _context.RouteLikes.FindAsync(id);

            if (like == null)
                return false;

            _context.RouteLikes.Remove(like);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}