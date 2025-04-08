using DogusBlog.Models;
using DogusBlog.Repositories;

namespace DogusBlog.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllWithCategoryAsync()
        {
            return await _blogRepository.GetAllWithCategoryAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task<Blog> GetBlogWithDetailsAsync(int id)
        {
            return await _blogRepository.GetBlogWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetByCategoryAsync(int categoryId)
        {
            return await _blogRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task AddAsync(Blog blog)
        {
            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveAsync();
        }

        public async Task UpdateAsync(Blog blog)
        {
            _blogRepository.Update(blog);
            await _blogRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog != null)
            {
                _blogRepository.Delete(blog);
                await _blogRepository.SaveAsync();
            }
        }
    }


}
