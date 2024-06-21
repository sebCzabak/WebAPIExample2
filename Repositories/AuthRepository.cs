using WebAPIExample2.Data;
using WebAPIExample2.Interfaces;
using WebAPIExample2.Models;

namespace WebAPIExample2.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        public AuthRepository(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }
        public async Task<User> Login(LoginModel loginModel)
        {
            return await _userRepository.GetUser(loginModel);

        }
        public async Task<bool> Register(User userModel)
        {
            return await _userRepository.AddUser(userModel);
        }
    }
}
