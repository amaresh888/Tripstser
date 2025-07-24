using AirbnbApi.DTO;
using AirbnbApi.Models;

namespace AirbnbApi.Repository.Interface
{
   
    namespace AirbnbApi.Repository
    {
        public interface IBookingRepository
        {
            List<Booking> GetAll();
            Booking Get(int id);
            BookingDTO Create(BookingDTO book);
            Booking Update(int id, BookingDTO book);
            Booking Delete(int id);
        }
    }

}
