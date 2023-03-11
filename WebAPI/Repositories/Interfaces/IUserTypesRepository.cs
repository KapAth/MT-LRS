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
        /// <returns></returns>
        Task<List<UserType>> GetAllUserTypesAsync();

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<UserType> GetUserTypeAsync(int id);
    }
}