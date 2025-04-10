using DogusBlog.Models;
using DogusBlog.Repositories;
using Microsoft.EntityFrameworkCore;

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
       
        public async Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId)
        {
            return await _blogRepository.GetBlogsByUserIdAsync(userId);
        }


       
        public async Task ClearBlogTagsAsync(int blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog != null && blog.BlogTags != null)
            {
                blog.BlogTags.Clear();
                 _blogRepository.Update(blog);
            }
        }
        
        public async Task UpdateBlogTagsAsync(int blogId, List<int> tagIds)
        {
            
            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog == null)
                throw new KeyNotFoundException($"BlogId {blogId} bulunamadı.");

            
            var dbContext = _blogRepository.GetDbContext() as ApplicationDbContext; 
            if (dbContext == null)
                throw new InvalidOperationException("Veritabanı bağlantısı sağlanamadı.");

           
            var existingTags = dbContext.BlogTags.Where(bt => bt.BlogId == blogId).ToList();
            dbContext.BlogTags.RemoveRange(existingTags);
            await dbContext.SaveChangesAsync();

           
            foreach (var tagId in tagIds)
            {
                dbContext.BlogTags.Add(new BlogTag
                {
                    BlogId = blogId,
                    TagId = tagId
                });
            }
            await dbContext.SaveChangesAsync();
        }


    }


}
