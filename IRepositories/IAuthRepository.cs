using WebAPIExample2.Models;

namespace WebAPIExample2.Interfaces
{
    public interface IAuthRepository
    {
        public Task<User> Login(LoginModel loginModel);
        public Task<bool> Register(User userModel);

    }
}
