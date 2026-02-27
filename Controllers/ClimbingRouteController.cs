using Microsoft.AspNetCore.Mvc;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClimbingRouteController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<ClimbingRouteController> _logger;
        private readonly ClimbingRouteService _routeService;

        public ClimbingRouteController(
            GymDbContext context,
            ILogger<ClimbingRouteController> logger,
            ClimbingRouteService routeService)
        {
            _context = context;
            _logger = logger;
            _routeService = routeService;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all climbing routes",
            Description = "Retrieves all climbing routes currently available in the gym."
        )]
        [SwaggerResponse(200, "Routes retrieved successfully")]
        public async Task<ActionResult<IEnumerable<ClimbingRouteDTO>>> GetRoutes()
        {
            _logger.LogInformation("Retrieved all routes");

            var routes = await _routeService.GetAllRoutes();

            var routeDTOs = routes.Select(route => new ClimbingRouteDTO
            {
                Id = route.Id,
                Location = route.Location,
                Difficulty = route.Difficulty,
                SetByStaffId = route.SetByStaffId,
                DateSet = route.DateSet,
                StripDate = route.StripDate
            }).ToList();

            return routeDTOs;
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create climbing route",
            Description = "Creates a new climbing route in the gym. Admin access required."
        )]
        [SwaggerResponse(201, "Route created successfully")]
        [SwaggerResponse(400, "Invalid route data")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        public async Task<ActionResult<ClimbingRoute>> CreateRoute(CreateClimbingRouteDTO dto)
        {
            _logger.LogInformation("Creating new route");

            var route = new ClimbingRoute
            {
                Location = dto.Location,
                Difficulty = dto.Difficulty,
                SetByStaffId = dto.SetByStaffId,
                DateSet = dto.DateSet,
                StripDate = dto.StripDate
            };

            var createdRoute = await _routeService.CreateRoute(route);

            return CreatedAtAction(nameof(GetRoutes), new { id = createdRoute.Id}, createdRoute);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete climbing route",
            Description = "Deletes a climbing route from the gym. Admin access required."
        )]
        [SwaggerResponse(204, "Route deleted")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Route not found")]
        public async Task<IActionResult> DeleteRoute(int id)
        {

            _logger.LogInformation("Deleting route");

            var success = await _routeService.DeleteRoute(id);

            if (!success)
                {
                _logger.LogWarning("Route not found");
                return NotFound();
                }

            _logger.LogInformation("Route deleted");

            return NoContent();
        }
    }
}