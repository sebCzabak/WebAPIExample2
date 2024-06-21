using WebAPIExample2.Models;

namespace WebAPIExample2.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUser(int userId);
        public Task<User> GetUser(LoginModel loginModel);
        public Task<IEnumerable<User>> GetUsers();
        public Task<bool> AddUser(User userModel);
        public Task UpdateUser(User userModel);
        public Task DeleteUser(int userId);
    }
}
