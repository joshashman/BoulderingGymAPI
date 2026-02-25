using System.ComponentModel.DataAnnotations;

namespace BoulderingGymAPI.DTOs
{
    public class CreateMembershipDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
    }

    public class MembershipDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}