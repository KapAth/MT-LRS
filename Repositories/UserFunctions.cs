using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Repository;
using Repositories.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Repositories
{
    // TODO naming classes and interfaces with related names to be obvious which implements what
    public class UserFunctions : IUser
    {
        private MTLRSDbContext _context;
        public UserFunctions()
        {
            // TODO context should be injectable
            _context = new MTLRSDbContext();
        }

        //TODO xml doc on interfaces
        /// <summary>Gets all users.</summary>
        /// <returns>
        /// A list of Users
        /// </returns>
        /// TODO why is this returning a DTO and all others a User?
        public async Task<List<UserDto>> GetAllUsers()
        {
            // TODO single dot per line is preferable
            var users = _context.User
                .Include(u => u.UserTitle)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    BirthDate = u.BirthDate,
                    EmailAddress = u.EmailAddress,
                    IsActive = u.IsActive,
                    Title = u.UserTitle.Description, // TODO UserTitle is nullable
                    Type = u.UserType.Description
                }).ToListAsync();

            return await users;
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns the <see cref="User"/> fetched by the identifier.
        /// </returns>
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);

            return user; // TODO this can be null
        }//TODO single empty line


        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   User
        /// </returns>
        /// TODO the id is not used
        public async Task<User> PutUser(int id, User user)
        {
// TODO you need to first find the entity, then update it accordingly
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO when does this occur and what's the result?
                return null;
            }

            return user;
        }

        /// <summary>Adds a new user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///  User
        /// </returns>
        public async Task<User> AddNewUser(User user)
        {
            // TODO remove commented code
            //User NewUser = new User
            //{
            //    Name = user.Name,
            //    Surname = user.Surname,
            //    BirthDate = user.BirthDate,
            //    UserTypeId = user.UserTypeId,
            //    UserTitleId = user.UserTitleId,
            //    EmailAddress = user.EmailAddress,
            //    IsActive = user.IsActive
            //};


            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            // TODO no point in returning the same user param, should return what is provided by AddAsync
            return user;
        }

        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br /> TODO missing
        /// </returns>
        /// TODO if the value is nullable, then declare it so
        public async Task<User> DeleteUser(int id)
        {

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                // TODO maybe there should be an error if no user found with this id?
                return null;
            }

            // TODO the exercise says that deletion is logical not actual
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}