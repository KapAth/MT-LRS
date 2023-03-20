using Repositories.Repository.Entities;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IUsersService
    {
        /// <summary>
        /// Adds the new user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        Task AddNewUserDtoAsync(UserDto userDto);

        /// <summary>
        /// Gets all users dto asynchronous.
        /// </summary>
        /// <param name="filter">The filter for name or surname (optional).</param>
        /// <returns> A list of Users. <see cref="UserDto"/></returns>
        Task<List<UserDto>> GetAllUsersDtoAsync(string? filter = null);

        /// <summary>
        /// Gets the user dto by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A single User. <see cref="UserDto"/></returns>
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