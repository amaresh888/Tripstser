using AirbnbApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AirbnbApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       public DbSet<User> users { get; set; }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Booking> bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
        new Hotel
        {
            PropertyId = 1,
            Title = "Sunrise Paradise",
            Description = "Beach-facing villa with stunning sunrise views.",
            Location = "Goa",
            PricePerNight = 2500
        },
        new Hotel
        {
            PropertyId = 2,
            Title = "Himalayan Retreat",
            Description = "Cozy cottage in the heart of the mountains.",
            Location = "Manali",
            PricePerNight = 3000
        },
        new Hotel
        {
            PropertyId = 3,
            Title = "City Luxe Hotel",
            Description = "Luxury suite with skyline views.",
            Location = "Mumbai",
            PricePerNight = 5000
        },
        new Hotel
        {
            PropertyId = 4,
            Title = "Desert Safari Stay",
            Description = "Authentic Rajasthani tents in the Thar desert.",
            Location = "Jaisalmer",
            PricePerNight = 2000
        });


            base.OnModelCreating(modelBuilder);
        }
    }
}
