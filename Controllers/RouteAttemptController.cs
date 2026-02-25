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
    public class RouteAttemptController : ControllerBase
    {
        private readonly GymDbContext _context;

        private readonly ILogger<RouteAttemptController> _logger;

        public RouteAttemptController(
            GymDbContext context,
            ILogger<RouteAttemptController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteAttemptDTO>>> GetAttempts()
        {
            _logger.LogInformation("Retrieved all route attempts");

            var attempts = await _context.RouteAttempts.ToListAsync();

            var attemptDTOs = attempts.Select(a => new RouteAttemptDTO
            {
                Id = a.Id,
                UserId = a.UserId,
                ClimbingRouteId = a.ClimbingRouteId,
                AttemptDate = a.AttemptDate,
                Completed = a.Completed
            }).ToList();

            return attemptDTOs;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RouteAttempt>> CreateAttempt(CreateRouteAttemptDTO dto)
        {
            _logger.LogInformation("Creating route attempt");

            var attempt = new RouteAttempt
            {
            UserId = dto.UserId,
            ClimbingRouteId = dto.ClimbingRouteId,
            AttemptDate = dto.AttemptDate,
            Completed = dto.Completed
            };

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
            {
                _logger.LogWarning("Route attempt not found");
                return NotFound();
            }

            _context.RouteAttempts.Remove(attempt);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Route attempt deleted");

            return NoContent();
        }
    }
}