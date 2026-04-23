using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class RouteLike
    {
        public int Id {get; set;}

        public string UserId {get; set;} = null!;

        [ForeignKey("UserId")]
        public ApplicationUser User {get; set;} = null!;

        public int ClimbingRouteId {get; set;}

        [ForeignKey("ClimbingRouteId")]
        public ClimbingRoute CLimbingRoute {get; set;} = null!;
    }
}