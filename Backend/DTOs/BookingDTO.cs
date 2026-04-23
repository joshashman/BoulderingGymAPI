using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateBookingDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int SessionId { get; set; }

        public decimal Price { get; set; }
    }

     public class BookingDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int SessionId { get; set; }

        public decimal Price { get; set; }
    }
}