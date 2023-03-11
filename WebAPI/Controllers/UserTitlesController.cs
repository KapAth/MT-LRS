using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTitlesController : ControllerBase
    {
        private readonly UserTitlesService _userTitlesService;

        public UserTitlesController(UserTitlesService userTitleService)
        {
            _userTitlesService = userTitleService;
        }

        // GET: api/UserTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTitleDto>>> GetUserTitles()
        {
            return await _userTitlesService.GetAllUserTitlesDtoAsync();
        }

        // GET: api/UserTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTitleDto>> GetUserTitle(int id)
        {
            return await _userTitlesService.GetUserTitleDtoByIdAsync(id);
        }
    }
}