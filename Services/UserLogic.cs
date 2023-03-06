using Repositories;
using Repositories.Interfaces;
using Repositories.Repository;
using Repositories.Repository.Entities;

namespace Services
{
    public class UserLogic
    {
        // TODO this should be implementing an interface
        // TODO suggested be named UserService
        // TODO avoid abbreviations.
        // TODO read about dependency injection, you shouldn't create a new instance yourself
        public IUser _DAL = new UserFunctions();


        public UserLogic()
        {
            
        }

        /// <summary>Gets all users.</summary>
        /// <returns>
        ///  A list of Users
        /// </returns>
        /// TODO single empty lines
        public async Task<List<UserDto>> GetAllUsers()
        {
            
           
            return await _DAL.GetAllUsers();
            //  return await _context.User.ToListAsync(); // TODO don't leave commented code in finalized code
        }

        /// <summary>Gets the user by ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   User
        /// </returns>
        public async Task<User> GetUserById(int id)
        {
            if (id <= 0)
            {
                // TODO argument checks
                throw new ArgumentOutOfRangeException(nameof(id));
            }

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
            // TODO missing business validations
            return await _DAL.PutUser(id, user);
        }

        /// <summary>Adds a new user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///  User TODO wrong return type
        /// </returns>
        public async Task<Boolean> AddNewUser(User user)
        {
            // TODO missing business validations
            try
            {
                var result = await _DAL.AddNewUser(user);
                if (result.Id > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // TODO what are you doing with the message?
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