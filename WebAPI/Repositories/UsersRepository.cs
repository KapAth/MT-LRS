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

        /// <summary>Gets all users.</summary>
        /// <returns>
        /// A list of Users <see cref="User"/>
        /// </returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.User.ToListAsync();

            return users;
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns the <see cref="User"/> fetched by the identifier.
        /// </returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            return user;
        }

        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
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

        /// <summary>Adds a new user.</summary>
        /// <param name="user">The user.</param>
        public async Task AddNewUserAsync(User user)
        {
            await _context.User.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
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