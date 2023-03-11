using Repositories.Repository.Entities;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Adds the new user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        Task AddNewUserDtoAsync(UserDto userDto);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// A list of Users, Type :<see cref="UserDto" />
        /// </returns>
        Task<List<UserDto>> GetAllUsersDtoAsync();

        /// <summary>
        /// Gets the user dto by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><see cref="UserDto"/></returns>
        Task<UserDto> GetUserDtoByIdAsync(int id);

        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        Task PutUserDtoAsync(int id, UserDto userDto);

        /// <summary>Deletes the user by setting isActive value to false.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteUserAsync(int id);
    }
}