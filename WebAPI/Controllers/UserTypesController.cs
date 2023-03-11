using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly UserTypesService _userTypesService;

        public UserTypesController(UserTypesService userTypesService)
        {
            _userTypesService = userTypesService;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeDto>>> GetUserTypes()
        {
            return await _userTypesService.GetAllUserTypesDtoAsync();
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDto>> GetUserType(int id)
        {
            return await _userTypesService.GetUserTypeDtoByIdAsync(id);
        }
    }
}