using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly GymDbContext _context;

        public MembershipController(GymDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membership>>> GetMemberships()
        {
            return await _context.Memberships.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Membership>> CreateMembership(Membership membership)
        {
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof (GetMemberships), new { id = membership.Id }, membership);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
                return NotFound();

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();

            return NoContent();    
        }
    }
}