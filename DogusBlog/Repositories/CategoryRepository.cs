using DogusBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryWithBlogsAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.Blogs)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }

}
