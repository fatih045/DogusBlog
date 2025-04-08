using DogusBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllWithCategoryAsync()
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<Blog> GetBlogWithDetailsAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.User)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Blog>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Blogs
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }
    }

}
