using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class ClimbingRouteService
    {
        private readonly GymDbContext _context;

        public ClimbingRouteService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClimbingRoute>> GetAllRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task<ClimbingRoute> CreateRoute(ClimbingRoute route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<bool> DeleteRoute(int id)
        {
            var route = await _context.Routes.FindAsync(id);

            if (route == null)
                return false;

            _context.Routes.Remove(route);

            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}