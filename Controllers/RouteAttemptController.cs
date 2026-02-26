using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;
using BoulderingGymAPI.Services;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteAttemptController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<RouteAttemptController> _logger;
        private readonly RouteAttemptService _attemptService;

        public RouteAttemptController(
            GymDbContext context,
            ILogger<RouteAttemptController> logger,
            RouteAttemptService attemptService)
        {
            _context = context;
            _logger = logger;
            _attemptService = attemptService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteAttemptDTO>>> GetAttempts()
        {
            _logger.LogInformation("Retrieved all route attempts");

            var attempts = await _attemptService.GetAllAttempts();

            var attemptDTOs = attempts.Select(attempt => new RouteAttemptDTO
            {
                Id = attempt.Id,
                UserId = attempt.UserId,
                ClimbingRouteId = attempt.ClimbingRouteId,
                AttemptDate = attempt.AttemptDate,
                Completed = attempt.Completed
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

            var createdAttempt = await _attemptService.CreateAttempt(attempt);

            return CreatedAtAction(nameof(GetAttempts), new { id = createdAttempt.Id }, createdAttempt);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttempt(int id)
        {
            _logger.LogInformation("Deleting route attempt");

            var success = await _attemptService.DeleteAttempt(id);

            if (!success)
            {
                _logger.LogWarning("Route attempt not found");
                return NotFound();
            }

            _logger.LogInformation("Route attempt deleted");

            return NoContent();
        }
    }
}