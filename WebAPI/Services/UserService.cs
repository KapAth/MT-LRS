using AutoMapper;
using Repositories.Repository.Entities;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUsersRepository userRepo, IMapper mapper)
        {
            _userRepository = userRepo;
            _mapper = mapper;
        }

        /// <summary>Gets all users.</summary>
        /// <returns>
        ///  A list of Users <see cref="User"/>
        /// </returns>
        public async Task<List<UserDto>> GetAllUsersDtoAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        /// <summary>Gets the user by ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///  A Single <see cref="User"/>
        /// </returns>
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

        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        public async Task PutUserDtoAsync(int id, UserDto userDto)
        {
            if (id <= 0 || id != userDto.Id)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }
            User user = _mapper.Map<User>(userDto);
            await _userRepository.PutUserAsync(id, user);
        }

        /// <summary>
        /// Adds the new user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">
        /// UserTypeId - Invalid UserTypeId
        /// or
        /// UserTitleId - Invalid UserTitleId
        /// or
        /// UserTitleId - Email Address cannot be greater than 50 characters
        /// or
        /// Invalid User
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name - Name and Surname length cannot be greater than 20 characters</exception>
        public async Task AddNewUserDtoAsync(UserDto userDto)
        {
            if (userDto != null)
            {
                if (userDto.UserTypeId <= 0)
                {
                    throw new ArgumentNullException(nameof(userDto.UserTypeId), "Invalid UserTypeId");
                }

                if (userDto.UserTitleId <= 0)
                {
                    throw new ArgumentNullException(nameof(userDto.UserTitleId), "Invalid UserTitleId");
                }

                if (!string.IsNullOrEmpty(userDto.Name) && !string.IsNullOrEmpty(userDto.Surname) && (userDto.Name.Length > 20 || userDto.Surname.Length > 20))
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.Name), "Name and Surname length cannot be greater than 20 characters");
                }

                if (!string.IsNullOrEmpty(userDto.EmailAddress) && userDto.EmailAddress.Length > 50)
                {
                    throw new ArgumentOutOfRangeException(nameof(userDto.UserTitleId), "Email Address cannot be greater than 50 characters");
                }
                User user = _mapper.Map<User>(userDto);
                await _userRepository.AddNewUserAsync(user);
            }
            else
            {
                throw new ArgumentNullException("Invalid User");
            }
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">id - Invalid ID</exception>
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