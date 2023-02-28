using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Repositories.Repository;
using Repositories.Repository.Entities;

using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private Services.UserLogic _BLL;
        private readonly ILogger<UsersController> _logger;


        public UsersController(ILogger<UsersController> logger)
        {
            _BLL = new Services.UserLogic();
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
        {
            return await _BLL.GetAllUsers();
            //return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _BLL.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("User {id} not found", id);
                return BadRequest("Invalid ID");
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var _user = await _BLL.PutUser(id, user);

            if (_user == null)
            {
                _logger.LogWarning("User {id} not found", id);
                return BadRequest("Invalid ID");
            }

            return Ok(user);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _BLL.AddNewUser(user);
            //_context.User.Add(user);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _BLL.DeleteUser(id);
            if (user == null)
            {
                _logger.LogWarning("User {id} not found", id);
                return BadRequest("Invalid ID");
            }
            return Ok(user);
        }

       
    }
}
