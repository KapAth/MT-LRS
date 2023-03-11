using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IUserTypesService
    {
        /// <summary>
        /// Gets the user types asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<UserTypeDto>> GetAllUserTypesDtoAsync();

        /// <summary>
        /// Gets the user type asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<UserTypeDto> GetUserTypeDtoByIdAsync(int id);
    }
}