using WebAPIExample2.Models;

namespace WebAPIExample2.IServices
{
    public interface IUserService
    {
        public Task<User> GetUser(int userId);
        public Task<IEnumerable<User>> GetUsers();
        public Task<bool> AddUser(User userModel);
        public Task UpdateUser(User userModel);
        public Task DeleteUser(int userId);
    }
}
