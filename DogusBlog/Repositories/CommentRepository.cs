using DogusBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId)
                .Include(c => c.User)
                .ToListAsync();
        }
    }

}
