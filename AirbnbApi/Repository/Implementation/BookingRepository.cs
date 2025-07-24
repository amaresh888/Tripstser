

using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using AirbnbApi.Repository.Interface;
using AirbnbApi.Repository.Interface.AirbnbApi.Repository;

namespace AirbnbApi.Repository.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public BookingDTO Create(BookingDTO book)
        {
            var booking = new Booking
            {
                UserId = book.UserId,
                PropertyId = book.PropertyId,
                CheckInDate = book.CheckInDate,
                CheckOutDate = book.CheckOutDate
            };

            _db.bookings.Add(booking);
            _db.SaveChanges();
            book.Id = booking.Id;
            return book;
        }

        public Booking Delete(int UserId)
        {
            var booking = _db.bookings.Find(UserId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {UserId} not found.");
            }

            _db.bookings.Remove(booking);
            _db.SaveChanges();

            return booking;
        }

        public Booking Get(int UserId)
        {
            var booking = _db.bookings.Find(UserId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {UserId} not found.");
            }
            return booking;
        }

        public List<Booking> GetAll()
        {
            return _db.bookings.ToList();
        }

        public Booking Update(int UserId, BookingDTO book)
        {
            var existing = _db.bookings.Find(UserId);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Booking with ID {UserId} not found.");
            }
            

            existing.UserId = book.UserId;
            existing.PropertyId = book.PropertyId;
            existing.CheckInDate = book.CheckInDate;
            existing.CheckOutDate = book.CheckOutDate;
            _db.bookings.Update(existing);

            _db.SaveChanges();
            return existing;
        }
    }
}
