using Repositories.Repository;
using Repositories.Repository.Entities;

namespace Repositories.Interfaces
{
    // TODO has to do with a repository not an entity repository, name it as such i.e. IUserRepository
    public interface IUser
    {
        /// <summary>
        /// TODO xml doc
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //Expose the functions required
        // TODO suffix with Async all async methods
        Task<User> AddNewUser(User user);

        Task<List<UserDto>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> PutUser(int id, User user);

        Task<User> DeleteUser(int id);
    }
}