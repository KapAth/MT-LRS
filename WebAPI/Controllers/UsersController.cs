using Microsoft.AspNetCore.Mvc;
using Repositories.Repository;
using Repositories.Repository.Entities;
using Services;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userservice)
        {
            _userService = userservice;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
        {
            return await _userService.GetAllUsersDtoAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return await _userService.GetUserDtoByIdAsync(id);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            await _userService.PutUserDtoAsync(id, userDto);
            return Ok();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDto userDto)
        {
            await _userService.AddNewUserDtoAsync(userDto);
            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}