using Microsoft.AspNetCore.Mvc;
using Repositories.Repository.Entities;
using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUserTypesRepository
    {
        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>A list of User Types <see cref="UserType"/></returns>
        Task<List<UserType>> GetAllUserTypesAsync();

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A single <see cref="UserType"/></returns>
        Task<UserType> GetUserTypeAsync(int id);
    }
}