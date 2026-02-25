using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteLikeController : ControllerBase
    {
        private readonly GymDbContext _context;

        public RouteLikeController(GymDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteLike>>> GetLikes()
        {
            return await _context.RouteLikes.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RouteLike>> CreateLike(RouteLike like)
        {
            _context.RouteLikes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikes), new { id = like.Id }, like);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await _context.RouteLikes.FindAsync(id);

            if (like == null)
                return NotFound();

            _context.RouteLikes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}