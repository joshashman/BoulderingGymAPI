using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateSessionDTO
    {
        [Required]
        public string InstructorId { get; set; } = string.Empty;

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }

    public class SessionDTO
    {
        public int Id { get; set; }

        public string InstructorId { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}