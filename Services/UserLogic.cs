using Repositories;
using Repositories.Interfaces;
using Repositories.Repository;
using Repositories.Repository.Entities;

namespace Services
{
    public class UserLogic
    {
        public IUser _DAL = new Repositories.UserFunctions();


        public UserLogic()
        {
            
        }

        /// <summary>Gets all users.</summary>
        /// <returns>
        ///  A list of Users
        /// </returns>
        public async Task<List<UserDto>> GetAllUsers()
        {
            
           
            return await _DAL.GetAllUsers();
            //  return await _context.User.ToListAsync();
        }

        /// <summary>Gets the user by ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   User
        /// </returns>
        public async Task<User> GetUserById(int id)
        {
            return await _DAL.GetUserById(id);


        }

        /// <summary>Updates the user with ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///  User
        /// </returns>
        public async Task<User> PutUser(int id, User user)
        {
            return await _DAL.PutUser(id, user);
        }

        /// <summary>Adds a new user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///  User
        /// </returns>
        public async Task<Boolean> AddNewUser(User user)
        {
            try
            {
                var result = await _DAL.AddNewUser(user);
                if (result.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return false;
            }

        }
        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   User
        /// </returns>
        public async Task<User> DeleteUser(int id)
        {
            return await _DAL.DeleteUser(id);
        }
    }
}
