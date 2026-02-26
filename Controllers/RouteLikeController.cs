using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;
using BoulderingGymAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Get route likes",
            Description = "Retrieves all route likes."
        )]
        [SwaggerResponse(200, "Likes retrieved successfully")]
        [SwaggerResponse(401, "Unauthorized")]
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
        [SwaggerOperation(
            Summary = "Like a route",
            Description = "Records a user's like for a climbing route."
        )]
        [SwaggerResponse(201, "Like created successfully")]
        [SwaggerResponse(400, "Invalid like data")]
        [SwaggerResponse(401, "Unauthorized")]
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
        [SwaggerOperation(
            Summary = "Delete a like",
            Description = "Deletes a route like."
        )]
        [SwaggerResponse(204, "Like deleted")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Like not found")]
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