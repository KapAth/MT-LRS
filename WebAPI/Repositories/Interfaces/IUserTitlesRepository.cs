using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUserTitlesRepository
    {
        /// <summary>
        /// Gets all user titles asynchronous.
        /// </summary>
        /// <returns>A list of User Titles. <see cref="UserTitle"/></returns>
        Task<List<UserTitle>> GetAllUserTitlesAsync();

        /// <summary>
        /// Gets the user title by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> A Single User Title. <see cref="UserTitle"/></returns>
        Task<UserTitle> GetUserTitleByIdAsync(int id);
    }
}