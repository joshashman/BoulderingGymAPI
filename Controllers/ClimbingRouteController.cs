using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClimbingRouteController : ControllerBase
    {
        private readonly GymDbContext _context;

        public ClimbingRouteController(GymDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClimbingRoute>>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult<ClimbingRoute>> CreateRoute(ClimbingRoute route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoutes), new { id = route.Id}, route);
        }
    }
}