using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class UserTitlesService : IUserTitlesService
    {
        private readonly IUserTitlesRepository _userTitlesRepository;
        private readonly IMapper _mapper;

        public UserTitlesService(IUserTitlesRepository userTitlesRepo, IMapper mapper)
        {
            _userTitlesRepository = userTitlesRepo ?? throw new ArgumentNullException(nameof(userTitlesRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserTitleDto>> GetAllUserTitlesDtoAsync()
        {
            var userTitles = await _userTitlesRepository.GetAllUserTitlesAsync();
            var userTitlesDto = _mapper.Map<List<UserTitleDto>>(userTitles);
            return userTitlesDto;
        }

        public async Task<UserTitleDto> GetUserTitleDtoByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }
            var userTitle = await _userTitlesRepository.GetUserTitleByIdAsync(id);
            var userTitleDto = _mapper.Map<UserTitleDto>(userTitle);
            return userTitleDto;
        }
    }
}