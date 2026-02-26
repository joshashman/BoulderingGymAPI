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
    public class SessionController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<SessionController> _logger;
        private readonly SessionService _sessionService;

        public SessionController(
            GymDbContext context,
            ILogger<SessionController> logger,
            SessionService sessionService)
        {
            _context = context;
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all sessions",
            Description = "Retrieves all coaching sessions available at the gym."
        )]
        [SwaggerResponse(200, "Sessions retrieved successfully")]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions()
        {
            _logger.LogInformation("Retrieved all sessions");

            var sessions = await _sessionService.GetAllSessions();

            var sessionDTOs = sessions.Select(session => new SessionDTO
            {
                Id = session.Id,
                InstructorId = session.InstructorId,
                MaxCapacity = session.MaxCapacity,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Description = session.Description
            }).ToList();

            return sessionDTOs;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a session",
            Description = "Creates a new coaching session. Admin access required."
        )]
        [SwaggerResponse(201, "Session created successfully")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        public async Task<ActionResult<Session>> CreateSession(CreateSessionDTO dto)
        {
            _logger.LogInformation("Creating new session");
            
            var session = new Session
            {
                MaxCapacity = dto.MaxCapacity,
                InstructorId = dto.InstructorId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Description = dto.Description
            };

            var createdSession = await _sessionService.CreateSession(session);

            return CreatedAtAction(nameof(GetSessions), new { id = createdSession.Id}, createdSession);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a session",
            Description = "Deletes a coaching session. Admin access required."
        )]
        [SwaggerResponse(204, "Session deleted")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Session not found")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            _logger.LogInformation("Deleting session");

            var success = await _sessionService.DeleteSession(id);

            if (!success)
            {
                _logger.LogWarning("Session not found");
                return NotFound();
            }

            _logger.LogInformation("Session deleted");

            return NoContent();    
        }
    }
}