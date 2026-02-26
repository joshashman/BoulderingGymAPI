using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class BookingService
    {
        private readonly GymDbContext _context;

        public BookingService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}