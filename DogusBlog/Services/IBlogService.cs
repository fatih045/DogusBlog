using DogusBlog.Models;

namespace DogusBlog.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<IEnumerable<Blog>> GetAllWithCategoryAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> GetBlogWithDetailsAsync(int id);
        Task<IEnumerable<Blog>> GetByCategoryAsync(int categoryId);
        Task AddAsync(Blog blog);
        Task UpdateAsync(Blog blog);
        Task DeleteAsync(int id);


        Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId);

        Task ClearBlogTagsAsync(int blogId);

        Task UpdateBlogTagsAsync(int blogId, List<int> tagIds);
    }

}
