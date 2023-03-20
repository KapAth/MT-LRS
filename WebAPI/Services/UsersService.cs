using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Repositories.Repository.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository userRepo, IMapper mapper)
        {
            _userRepository = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserDto>> GetAllUsersDtoAsync(string? filter = null)
        {
            var users = await _userRepository.GetAllUsersAsync(filter);
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task PutUserDtoAsync(int id, UserDto userDto)
        {
            if (id <= 0 ||
                id != userDto.Id)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }

            User user = _mapper.Map<User>(userDto);
            await _userRepository.PutUserAsync(id, user);
        }

        public async Task AddNewUserDtoAsync(UserDto userDto)
        {
            if (userDto != null)
            {
                if (userDto.UserTypeId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.UserTypeId), "Invalid UserTypeId");
                }

                if (userDto.UserTitleId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.UserTitleId), "Invalid UserTitleId");
                }

                if (!string.IsNullOrEmpty(userDto.Name) &&
                    !string.IsNullOrEmpty(userDto.Surname) &&
                    (userDto.Name.Length > 20 || userDto.Surname.Length > 20))
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.Name),
                        "Name and Surname length cannot be greater than 20 characters");
                }

                if (!string.IsNullOrEmpty(userDto.EmailAddress) &&
                    userDto.EmailAddress.Length > 50)
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.UserTitleId),
                        "Email Address cannot be greater than 50 characters");
                }

                User user = _mapper.Map<User>(userDto);
                await _userRepository.AddNewUserAsync(user);
            }
            else
            {
                throw new ArgumentNullException("Invalid User");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }

            await _userRepository.DeleteUserAsync(id);
        }
    }
}