using WebAPIExample2.Interfaces;
using WebAPIExample2.IServices;
using WebAPIExample2.JWT;
using WebAPIExample2.Models;

namespace WebAPIExample2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;

        }
        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _authRepository.Login(loginModel);
            if (user != null) 
            {
                return JWTGenerator.GenerateJSONWebToken(user, _configuration);
            }
            return "";

        }
        public async Task<bool> Register(User userModel)
        {
            return await _authRepository.Register(userModel);

        }
    }
}
