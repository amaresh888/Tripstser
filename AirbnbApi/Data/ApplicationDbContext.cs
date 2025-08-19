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
            PricePerNight = 2500,
            ImageUrl= "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644616/pexels-kelly-2869215_lla3mr.jpg"
        },
        new Hotel
        {
            PropertyId = 2,
            Title = "Himalayan Retreat",
            Description = "Cozy cottage in the heart of the mountains.",
            Location = "Manali",
            PricePerNight = 3000,
            ImageUrl = "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644760/pexels-boonkong-boonpeng-442952-1134176_stpwik.jpg"
        },
        new Hotel
        {
            PropertyId = 3,
            Title = "City Luxe Hotel",
            Description = "Luxury suite with skyline views.",
            Location = "Mumbai",
            PricePerNight = 5000,
            ImageUrl = "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644801/pexels-pixabay-261388_unh5uf.jpg"
        },
        new Hotel
        {
            PropertyId = 4,
            Title = "Desert Safari Stay",
            Description = "Authentic Rajasthani tents in the Thar desert.",
            Location = "Jaisalmer",
            PricePerNight = 2000,
            ImageUrl = "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644858/pexels-thorsten-technoman-109353-338504_amp2hh.jpg"
        });


            base.OnModelCreating(modelBuilder);
        }
    }
}
