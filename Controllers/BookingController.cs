using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;
using BoulderingGymAPI.DTOs;
using System.Security.Cryptography.X509Certificates;
using BoulderingGymAPI.Services;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly GymDbContext _context;
        private readonly ILogger<BookingController> _logger;
        private readonly BookingService _bookingService;

        public BookingController(
            GymDbContext context,
            ILogger<BookingController> logger,
            BookingService bookingService)
        {
            _context = context;
            _logger = logger;
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            _logger.LogInformation("Retrieved all bookings");

            var bookings = await _bookingService.GetAllBookings();

            var bookingDTOs = bookings.Select(booking => new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                SessionId = booking.SessionId,
                Price = booking.Price
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
            
            var createdBooking = await _bookingService.CreateBooking(booking);

            return CreatedAtAction(nameof(GetBookings), new { id = createdBooking.Id }, createdBooking);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            _logger.LogInformation("Deleting booking");

            var success = await _bookingService.DeleteBooking(id);

            if (!success)
            {
                _logger.LogWarning("Booking not found");
                return NotFound();
            }

            _logger.LogInformation("Booking deleted");

            return NoContent();    
        }
    }
}