using Repositories.Repository;
using Repositories.Repository.Entities;

namespace Repositories.Interfaces
{
    public interface IUser
    {
        //Expose the functions required
        Task<User> AddNewUser(User user);
        Task<List<UserDto>> GetAllUsers();
        Task<User> GetUserById(int id);

        Task<User> PutUser(int id, User user);
        Task<User> DeleteUser(int id);
    }
}
