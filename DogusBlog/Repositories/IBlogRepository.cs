using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface IBlogRepository :IGenericRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithCategoryAsync();
        Task<Blog> GetBlogWithDetailsAsync(int id);
        Task<IEnumerable<Blog>> GetByCategoryIdAsync(int categoryId);


    }
}
