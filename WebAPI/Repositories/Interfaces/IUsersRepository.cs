using Repositories.Repository.Entities;
using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        /// <summary>Adds a new user.</summary>
        /// <param name="user">The user.</param>
        Task AddNewUserAsync(User user);

        /// <summary>Gets all users.</summary>
        /// <returns>
        ///  A list of Users <see cref="User"/>
        /// </returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>Gets the user by ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///  A Single <see cref="User"/>
        /// </returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        Task PutUserAsync(int id, User user);

        /// <summary>Deletes the user by setting isActive value to false.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteUserAsync(int id);
    }
}