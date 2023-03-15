using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using Repositories.Repository.Entities;
using WebAPI.Repositories.Interfaces;

namespace Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private MTLRSDbContext _context;

        public UsersRepository(MTLRSDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync(string? filter = null)
        {
            IQueryable<User> query = _context.User;

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => (u.Name != null && u.Name.Contains(filter)) || (u.Surname != null && u.Surname.Contains(filter)));
            }

            var users = await query.ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            return user;
        }

        public async Task PutUserAsync(int id, User user)
        {
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewUserAsync(User user)
        {
            await _context.User.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}