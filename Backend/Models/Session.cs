using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderingGymAPI.Models
{
    public class Session
    {
        public int Id {get; set;}

        public int MaxCapacity {get; set;}
        
        public DateTime StartTime {get;set;}

        public DateTime EndTime {get; set;}

        public string Description {get; set;} = null!;
    }
}