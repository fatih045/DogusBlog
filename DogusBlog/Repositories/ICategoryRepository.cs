using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryWithBlogsAsync(int categoryId);



        Task<Category> GetByNameAsync(string name);

    }


}
