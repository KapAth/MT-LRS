using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUserTypesRepository
    {
        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>A list of User Types. <see cref="UserType"/></returns>
        Task<List<UserType>> GetAllUserTypesAsync();

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A single User Type. <see cref="UserType"/></returns>
        Task<UserType> GetUserTypeAsync(int id);
    }
}