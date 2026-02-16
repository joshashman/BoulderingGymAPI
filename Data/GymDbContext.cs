using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Data
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options)
            : base(options)
        {
        }
    }
}