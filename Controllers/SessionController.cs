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
    public class SessionController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<SessionController> _logger;

        public SessionController(
            GymDbContext context,
            ILogger<SessionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions()
        {
            _logger.LogInformation("Retrieved all sessions");

            var sessions = await _context.Sessions.ToListAsync();

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

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessions), new { id = session.Id}, session);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                _logger.LogWarning("Session not found");
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Session deleted");

            return NoContent();    
        }
    }
}