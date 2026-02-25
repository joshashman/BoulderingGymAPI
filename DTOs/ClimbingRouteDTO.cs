using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateClimbingRouteDTO
    {
        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Difficulty { get; set; } = string.Empty;

        [Required]
        public string SetByStaffId { get; set; } = string.Empty;

        [Required]
        public DateTime DateSet { get; set; }

        [Required]
        public DateTime StripDate { get; set; }
    }

    public class ClimbingRouteDTO
    {
        public int Id { get; set; }

        public string Location { get; set; } = string.Empty;

        public string Difficulty { get; set; } = string.Empty;

        public string SetByStaffId { get; set; } = string.Empty;

        public DateTime DateSet { get; set; }

        public DateTime? StripDate { get; set; }
    }
}