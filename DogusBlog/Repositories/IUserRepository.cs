using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        object GetByEmail(string email);
        Task<User> GetByEmailAsync(string email);
    }

}
