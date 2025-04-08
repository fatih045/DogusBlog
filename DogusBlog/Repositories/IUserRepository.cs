using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }

}
