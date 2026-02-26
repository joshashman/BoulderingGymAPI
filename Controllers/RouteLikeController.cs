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
    public class RouteLikeController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<RouteLikeController> _logger;
        private readonly RouteLikeService _likeService;

        public RouteLikeController(
            GymDbContext context,
            ILogger<RouteLikeController> logger,
            RouteLikeService likeService)
        {
            _context = context;
            _logger = logger;
            _likeService = likeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteLikeDTO>>> GetLikes()
        {
            _logger.LogInformation("Retrieved all route likes");

            var likes = await _likeService.GetAllLikes();

            var likeDTOs = likes.Select(like => new RouteLikeDTO
            {
                Id = like.Id,
                UserId = like.UserId,
                ClimbingRouteId = like.ClimbingRouteId
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

            var createdLike = await _likeService.CreateLike(like);

            return CreatedAtAction(nameof(GetLikes), new { id = createdLike.Id }, createdLike);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            _logger.LogInformation("Deleting route like");

            var success = await _likeService.DeleteLike(id);

            if (!success)
            {
                _logger.LogWarning("Route like not found");
                return NotFound();
            }

            _logger.LogInformation("Route like deleted");

            return NoContent();
        }
    }
}