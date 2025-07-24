using AirbnbApi.DTO;
using AirbnbApi.Models;

namespace AirbnbApi.Repository.Interface
{
    public interface IUserRepository
    {
       
            List<User> GetAll();
            User Get(int id);
            User Create(UserDTO userDTO);
            User Update(UserDTO userDTO);
            User Delete(int id);
        
    }
}
