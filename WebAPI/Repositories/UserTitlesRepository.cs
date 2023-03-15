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

        public async Task<List<UserTitle>> GetAllUserTitlesAsync()
        {
            var usersTitles = await _context.UserTitle.ToListAsync();

            return usersTitles;
        }

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