using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateRouteAttemptDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ClimbingRouteId { get; set; }

        [Required]
        public DateTime AttemptDate { get; set; }

        public bool Completed { get; set; }
    }

    public class RouteAttemptDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int ClimbingRouteId { get; set; }

        public DateTime AttemptDate { get; set; }

        public bool Completed { get; set; }
    }
}