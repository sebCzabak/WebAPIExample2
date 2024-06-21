using WebAPIExample2.Models;

namespace WebAPIExample2.IServices
{
    public interface IAuthService
    {
        public Task<string> Login(LoginModel loginModel);
        public Task<bool> Register(User userModel);
    }
}
