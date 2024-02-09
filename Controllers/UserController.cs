using Microsoft.AspNetCore.Mvc;
using _3abarni_backend.DTOs;
using _3abarni_backend.Services;

namespace _3abarni_backend.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        [HttpGet("search/{query}/{page}")]
        public IActionResult GetUsersQueryPaginated( string query, int page)
        {
            try
            {
                var users = _userService.SearchPaginated(query, page);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            _userService.Create(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] UserDto userDto)
        {
            _userService.Update(id, userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
