using DogusBlog.Models;

namespace DogusBlog.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }

}
