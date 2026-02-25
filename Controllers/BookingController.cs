using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            GymDbContext context,
            ILogger<BookingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            _logger.LogInformation("Retrieved all bookings");

            var bookings = await _context.Bookings.ToListAsync();

            var bookingDTOs = bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                UserId = b.UserId,
                SessionId = b.SessionId,
                Price = b.Price
            }).ToList();

            return bookingDTOs;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking (CreateBookingDTO dto)
        {
            _logger.LogInformation("Creating new booking");

            var booking = new Booking
            {
                UserId = dto.UserId,
                SessionId = dto.SessionId,
                Price = dto.Price
            };
            
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                _logger.LogWarning("Booking not found");
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Booking deleted");

            return NoContent();    
        }
    }
}