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
    public class UserFunctions : IUser
    {
        private MTLRSDbContext _context;
        public UserFunctions()
        {
            _context = new MTLRSDbContext();
        }

        /// <summary>Gets all users.</summary>
        /// <returns>
        /// A list of Users
        /// </returns>
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = _context.User.Include(u => u.UserTitle).Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                BirthDate = u.BirthDate,
                EmailAddress = u.EmailAddress,
                IsActive = u.IsActive,
                Title = u.UserTitle.Description,
                Type = u.UserType.Description

            }).ToListAsync();
       
            return await users;
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   User
        /// </returns>
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);

            return user;
        }


        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   User
        /// </returns>
        public async Task<User> PutUser(int id, User user)
        {

            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
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

            return user;
        }

        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<User> DeleteUser(int id)
        {

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
