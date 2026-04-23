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

        public DbSet<Membership> Memberships {get; set;}

        public DbSet<Session> Sessions {get; set;}

        public DbSet<Booking> Bookings {get; set;}

        public DbSet<ClimbingRoute> Routes {get; set;}

        public DbSet<RouteAttempt> RouteAttempts {get; set;}

        public DbSet<RouteLike> RouteLikes {get; set;}
    }
}