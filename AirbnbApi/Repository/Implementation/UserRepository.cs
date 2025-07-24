

using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using AirbnbApi.Repository.Interface;

namespace AirbnbApi.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public User Create(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            _db.users.Add(user);
            _db.SaveChanges();
            userDTO.UserId = user.UserId;
            return user;
        }

        public User Delete(int id)
        {
            
            var user = _db.users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            _db.users.Remove(user);
            _db.SaveChanges();
            return user;
        }

        public User Get(int id)
        {
            var user = _db.users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }

        public List<User> GetAll()
        {
           return _db.users.ToList();
        }

        public User Update(UserDTO userDTO)
        {
            var existingUser = _db.users.Find(userDTO.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {userDTO.UserId} not found.");
            }
            existingUser.Name = userDTO.Name;
            existingUser.Email = userDTO.Email;
            existingUser.Password = userDTO.Password;
            _db.users.Update(existingUser);
            _db.SaveChanges();
            return existingUser;
        }


    }
}
