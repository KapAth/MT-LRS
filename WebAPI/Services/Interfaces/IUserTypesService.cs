using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IUserTypesService
    {
        /// <summary>
        /// Gets the user types asynchronous.
        /// </summary>
        /// <returns>A list of User Types. <see cref="UserTypeDto"/></returns>
        Task<List<UserTypeDto>> GetAllUserTypesDtoAsync();

        /// <summary>
        /// Gets the user type asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A single User Type. <see cref="UserTypeDto"/></returns>
        Task<UserTypeDto> GetUserTypeDtoByIdAsync(int id);
    }
}