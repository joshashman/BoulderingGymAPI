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
    public class MembershipController : ControllerBase
    {
        private readonly GymDbContext _context;

        private readonly ILogger<MembershipController> _logger;

        public MembershipController(
            GymDbContext context,
            ILogger<MembershipController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> GetMemberships()
        {
            _logger.LogInformation("Retrieved all memberships");
            
            var memberships = await _context.Memberships.ToListAsync();

            var membershipDTOs = memberships.Select(m => new MembershipDTO
            {
                Id = m.Id,
                UserId = m.UserId,
                Type = m.Type,
                StartDate = m.StartDate,
                ExpiryDate = m.ExpiryDate
            }).ToList();

            return membershipDTOs;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Membership>> CreateMembership(CreateMembershipDTO dto)
        {
            _logger.LogInformation("Creating membership");
            
            var membership = new Membership
            {
                UserId = dto.UserId,
                Type = dto.Type,
                StartDate = dto.StartDate,
                ExpiryDate = dto.ExpiryDate
            };

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof (GetMemberships), new { id = membership.Id }, membership);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
            {
                _logger.LogWarning("Membership not found");
                return NotFound();
            }
            

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Membership deleted");

            return NoContent();    
        }
    }
}