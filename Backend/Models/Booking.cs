using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class Booking
    {
        public int Id {get; set;}

        public string UserId {get; set;} = null!;

        [ForeignKey("UserId")]
        public ApplicationUser User {get; set;} = null!;

        public int SessionId {get; set;}

        [ForeignKey ("SessionId")]
        public Session Session {get; set;} = null!;

        public decimal Price {get; set;}

        public DateTime CreatedAt {get; set;}
    }
}