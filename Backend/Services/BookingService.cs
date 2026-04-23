using BoulderingGymAPI.Models;
using BoulderingGymAPI.Repositories;

namespace BoulderingGymAPI.Services
{
    public class BookingService
    {
        private readonly IGenericRepository<Booking> _repository;

        public BookingService(IGenericRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _repository.GetAll();
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            return await _repository.Add(booking);
        }

        public async Task<bool> DeleteBooking(int id)
        {
            return await _repository.Delete(id);
        }
    }
}