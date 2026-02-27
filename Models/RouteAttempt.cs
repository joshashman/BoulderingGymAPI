using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class RouteAttempt
    {
        public int Id {get; set;}

        public string UserId {get; set;} = null!;

        [ForeignKey("UserId")]
        public ApplicationUser User {get; set;} = null!;

        public int ClimbingRouteId {get; set;}

        [ForeignKey("ClimbingRouteId")]
        public ClimbingRoute ClimbingRoute {get; set;} = null!;

        public DateTime AttemptDate {get; set;}

        public bool Completed {get; set;}
    }
}