using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByBlogIdAsync(int blogId);
    }

}
