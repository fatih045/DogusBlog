using DogusBlog.Models;

namespace DogusBlog.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int id);
    }
}
