using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class Membership
    {
        public int Id {get; set;}
        public string UserId {get; set;} = null!;
        
        [ForeignKey("UserId")]
        public ApplicationUser User {get;set;} = null!;
        public string Type {get; set;} = null!;
        public DateTime StartDate {get; set;}
        public DateTime ExpiryDate{get; set;}
    }
}