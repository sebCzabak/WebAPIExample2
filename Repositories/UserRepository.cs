using Microsoft.EntityFrameworkCore;
using WebAPIExample2.Data;
using WebAPIExample2.Interfaces;
using WebAPIExample2.Models;

namespace WebAPIExample2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dataContext.User.FindAsync(userId);
            return user != null ? user : null;
        }
        public async Task<User> GetUser(LoginModel loginModel)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            return user != null ? user : null;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dataContext.User.ToListAsync();
        }

        public async Task<bool> AddUser(User userModel)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Email == userModel.Email);
            if (user != null)
            {
                return false;
            }
            await _dataContext.AddAsync(userModel);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        public async Task UpdateUser(User userModel)
        {
            var user = await _dataContext.User.FindAsync(userModel.UserId);

            if (user != null)
            {
                user.Forename = userModel.Forename;
                user.Surname = userModel.Surname;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.Role = userModel.Role;

                await _dataContext.SaveChangesAsync();
            }

        }

        public async Task DeleteUser(int userId)
        {
            var user = await _dataContext.User.FindAsync(userId);

            if (user != null)
            {
                _dataContext.User.Remove(user);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
