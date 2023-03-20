using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTitlesController : ControllerBase
    {
        private readonly IUserTitlesService _userTitlesService;

        public UserTitlesController(IUserTitlesService userTitleService)
        {
            _userTitlesService = userTitleService ?? throw new ArgumentNullException(nameof(userTitleService));
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