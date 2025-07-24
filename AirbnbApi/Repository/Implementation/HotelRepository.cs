using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using AirbnbApi.Repository.Interface;
using AirbnbApi.Repository.Interface.AirbnbApi.Repository;

namespace AirbnbApi.Repository.Implementation
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _db;
        public HotelRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public HotelDTO Create(HotelDTO Hotel)
        {
            var hotel = new Hotel
            {
                PropertyId = Hotel.PropertyId,
                Title = Hotel.Title,
                Location = Hotel.Location,
                PricePerNight = Hotel.PricePerNight,
                Description = Hotel.Description

            };
            _db.hotels.Add(hotel);
            _db.SaveChanges();
            Hotel.PropertyId = hotel.PropertyId;
            return Hotel;
        }

        public Hotel Delete(int PropertyId)
        {
            var booking = _db.hotels.Find(PropertyId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Hotel with ID {PropertyId} not found.");
            }
            _db.hotels.Remove(booking);
            _db.SaveChanges();
            return booking;
        }

        public Hotel Get(int PropertyId)
        {
            var obj = _db.hotels.Find(PropertyId);
            if (obj == null)
            {
                throw new KeyNotFoundException($"Hotel with ID {PropertyId} not found.");
            }
            return obj;
        }

        public List<Hotel> GetAll()
        {
           
            return _db.hotels.ToList();
        }

        public Hotel Update(int id, HotelDTO Updated)
        {
            var existing = _db.hotels.Find(id);
            if(existing == null)
            {
                throw new KeyNotFoundException($"Hotel with ID {id} not found.");
            }
            existing.PropertyId = Updated.PropertyId;
            existing.Title = Updated.Title;
            existing.Location = Updated.Location;
            existing.PricePerNight = Updated.PricePerNight;
            existing.Description = Updated.Description;
            _db.hotels.Update(existing);
            _db.SaveChanges();
            return existing;

        }
    }
}

