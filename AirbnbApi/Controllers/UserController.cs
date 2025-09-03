using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _db.users.ToList();
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var obj = _db.users.Find(id);
            if (obj == null)
            {


                return NotFound($"User with ID {id} not found.");
            }
            return Ok(obj);
        }
        [HttpGet("UserByEmail/{email}")]
        public IActionResult Get(string email)
        {
            var obj = _db.users.FirstOrDefault(u=>u.Email == email);
            if (obj == null)
            {
                return NotFound($"User  not found.");
            }
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult Create([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null.");
            }
            var User = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber
            };
            _db.users.Add(User);
            _db.SaveChanges();
            userDTO.UserId = User.UserId;
            return Ok(userDTO.UserId);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UserDTO userDTO)
        {
            //if (userDTO == null)
            //{
            //    return BadRequest("User data is null.");
            //}
            var user = _db.users.Find(userDTO.UserId);
            //if (user == null)
            //{
            //    return NotFound($"User with ID {userDTO.UserId} not found.");
            //}
            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.PhoneNumber = userDTO.PhoneNumber;
            _db.Update(user);
            _db.SaveChanges();
            userDTO.UserId = user.UserId;
            return Ok(userDTO.UserId);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _db.users.Find(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            _db.users.Remove(user);
            _db.SaveChanges();
            return Ok($"User with ID {id} deleted successfully.");
        }
    }
}
