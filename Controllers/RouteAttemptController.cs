using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteAttemptController : ControllerBase
    {
        private readonly GymDbContext _context;

        public RouteAttemptController(GymDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteAttempt>>> GetAttempts()
        {
            return await _context.RouteAttempts.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RouteAttempt>> CreateAttempt(RouteAttempt attempt)
        {
            _context.RouteAttempts.Add(attempt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttempts), new { id = attempt.Id }, attempt);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttempt(int id)
        {
            var attempt = await _context.RouteAttempts.FindAsync(id);

            if (attempt == null)
                return NotFound();

            _context.RouteAttempts.Remove(attempt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}