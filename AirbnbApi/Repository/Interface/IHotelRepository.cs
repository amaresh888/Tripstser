using AirbnbApi.DTO;
using AirbnbApi.Models;

namespace AirbnbApi.Repository.Interface
{

    namespace AirbnbApi.Repository
    {
        public interface IHotelRepository
        {
            List<Hotel> GetAll();
            Hotel Get(int id);
            HotelDTO Create(HotelDTO book);
            Hotel Update(int id, HotelDTO book);
            Hotel Delete(int id);
        }
    }

}
