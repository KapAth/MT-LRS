using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using Repositories.Repository.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class UserTitlesRepository : IUserTitlesRepository
    {
        private MTLRSDbContext _context;

        public UserTitlesRepository(MTLRSDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all user titles asynchronous.
        /// </summary>
        /// <returns>
        /// A list of User Titles
        /// </returns>
        public async Task<List<UserTitle>> GetAllUserTitlesAsync()
        {
            var usersTitles = await _context.UserTitle.ToListAsync();

            return usersTitles;
        }

        /// <summary>
        /// Gets the user title by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// A Single <see cref="UserTitle" />
        /// </returns>
        /// <exception cref="System.ArgumentException">User not found</exception>
        public async Task<UserTitle> GetUserTitleByIdAsync(int id)
        {
            var userTitle = await _context.UserTitle.FindAsync(id);

            if (userTitle == null)
            {
                throw new ArgumentException("User Title not found");
            }

            return userTitle;
        }
    }
}