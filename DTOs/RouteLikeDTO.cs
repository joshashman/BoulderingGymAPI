using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateRouteLikeDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ClimbingRouteId { get; set; }
    }

    public class RouteLikeDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int ClimbingRouteId { get; set; }
    }
}