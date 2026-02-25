using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteLikeController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<RouteLikeController> _logger;

        public RouteLikeController(
            GymDbContext context,
            ILogger<RouteLikeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteLikeDTO>>> GetLikes()
        {
            _logger.LogInformation("Retrieved all route likes");

            var likes = await _context.RouteLikes.ToListAsync();

            var likeDTOs = likes.Select(l => new RouteLikeDTO
            {
                Id = l.Id,
                UserId = l.UserId,
                ClimbingRouteId = l.ClimbingRouteId
            }).ToList();

            return likeDTOs;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RouteLike>> CreateLike(CreateRouteLikeDTO dto)
        {
            _logger.LogInformation("Creating route like");

            var like = new RouteLike
            {
                UserId = dto.UserId,
                ClimbingRouteId = dto.ClimbingRouteId
            };

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
            {
                _logger.LogWarning("Route like not found");
                return NotFound();
            }

            _context.RouteLikes.Remove(like);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Route like deleted");

            return NoContent();
        }
    }
}