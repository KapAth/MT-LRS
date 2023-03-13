using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IUserTitlesService
    {
        /// <summary>
        /// Gets all user titles dto asynchronous.
        /// </summary>
        /// <returns>A list of User Titles <see cref="UserTitleDto"/></returns>
        Task<List<UserTitleDto>> GetAllUserTitlesDtoAsync();

        /// <summary>
        /// Gets the user title dto by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A single User Title<see cref="UserTitleDto"/></returns>
        Task<UserTitleDto> GetUserTitleDtoByIdAsync(int id);
    }
}