using WebAPIExample2.Interfaces;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUser(int userId)
        {
            return await _userRepository.GetUser(userId);
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
        public async Task<bool> AddUser(User userModel)
        {
            return await _userRepository.AddUser(userModel);
        }
        public async Task UpdateUser(User userModel)
        {
           await _userRepository.UpdateUser(userModel);
        }
        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }
    }
}
