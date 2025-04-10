using DogusBlog.Models;

namespace DogusBlog.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int id);
        Task<Tag> GetByNameAsync(string name);
    }
}
