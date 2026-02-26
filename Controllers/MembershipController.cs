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
    public class MembershipController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<MembershipController> _logger;
        private readonly MembershipService _membershipService;

        public MembershipController(
            GymDbContext context,
            ILogger<MembershipController> logger,
            MembershipService membershipService)
        {
            _context = context;
            _logger = logger;
            _membershipService = membershipService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> GetMemberships()
        {
            _logger.LogInformation("Retrieved all memberships");
            
            var memberships = await _membershipService.GetAllMemberships();

            var membershipDTOs = memberships.Select(membership => new MembershipDTO
            {
                Id = membership.Id,
                UserId = membership.UserId,
                Type = membership.Type,
                StartDate = membership.StartDate,
                ExpiryDate = membership.ExpiryDate
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

            var createdMembership = await _membershipService.CreateMembership(membership);

            return CreatedAtAction(nameof (GetMemberships), new { id = createdMembership.Id }, createdMembership);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            _logger.LogInformation("Deleting membership");

            var success = await _membershipService.DeleteMembership(id);

            if (!success)
            {
                _logger.LogWarning("Membership not found");
                return NotFound();
            }

            _logger.LogInformation("Membership deleted");

            return NoContent();    
        }
    }
}