using DogusBlog.Models;

namespace DogusBlog.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId);
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment comment);

        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<string?> GetCommentsByBlogIdAsync(int blogId);
    }

}
