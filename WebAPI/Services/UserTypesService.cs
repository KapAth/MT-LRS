using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class UserTypesService : IUserTypesService
    {
        private readonly IUserTypesRepository _userTypesRepository;
        private readonly IMapper _mapper;

        public UserTypesService(IUserTypesRepository userTypesRepo, IMapper mapper)
        {
            _userTypesRepository = userTypesRepo ?? throw new ArgumentNullException(nameof(userTypesRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserTypeDto>> GetAllUserTypesDtoAsync()
        {
            var userTypes = await _userTypesRepository.GetAllUserTypesAsync();
            var userTypesDto = _mapper.Map<List<UserTypeDto>>(userTypes);
            return userTypesDto;
        }

        public async Task<UserTypeDto> GetUserTypeDtoByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }
            var userType = await _userTypesRepository.GetUserTypeAsync(id);
            var userTypeDto = _mapper.Map<UserTypeDto>(userType);
            return userTypeDto;
        }
    }
}