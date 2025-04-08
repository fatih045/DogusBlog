using DogusBlog.Models;
using DogusBlog.Repositories;

namespace DogusBlog.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Task<List<Tag>> GetAllAsync() => _tagRepository.GetAllAsync();
        public Task<Tag?> GetByIdAsync(int id) => _tagRepository.GetByIdAsync(id);
        public Task AddAsync(Tag tag) => _tagRepository.AddAsync(tag);
        public Task UpdateAsync(Tag tag) => _tagRepository.UpdateAsync(tag);
        public Task DeleteAsync(int id) => _tagRepository.DeleteAsync(id);
    }
}
