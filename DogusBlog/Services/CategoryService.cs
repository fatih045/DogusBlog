using DogusBlog.Models;
using DogusBlog.Repositories;

namespace DogusBlog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> GetCategoryWithBlogsAsync(int id)
        {
            return await _categoryRepository.GetCategoryWithBlogsAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                await _categoryRepository.SaveAsync();
            }
        }
    }

}
