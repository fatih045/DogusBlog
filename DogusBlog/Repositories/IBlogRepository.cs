using DogusBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog.Repositories
{
    public interface IBlogRepository :IGenericRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithCategoryAsync();
        Task<Blog> GetBlogWithDetailsAsync(int id);
        Task<IEnumerable<Blog>> GetByCategoryIdAsync(int categoryId);

       
        Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId);

        DbContext GetDbContext();


    }
}
