using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class ClimbingRoute
    {
        public int Id {get; set;}
        public string Location {get; set;} = null!;
        
        public string Difficulty {get; set;} = null!;

        public string SetByStaffId {get; set;} = null!;

        [ForeignKey("SetByStaffId")]
        public ApplicationUser SetByStaff {get; set;} = null!;

        public DateTime DateSet {get; set;}

        public DateTime? StripDate {get; set;}
    }
}