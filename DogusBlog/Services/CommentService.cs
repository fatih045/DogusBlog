using DogusBlog.Models;
using DogusBlog.Repositories;

namespace DogusBlog.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId)
        {
            return await _commentRepository.GetCommentsByBlogIdAsync(blogId);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment != null)
            {
                _commentRepository.Delete(comment);
                await _commentRepository.SaveAsync();
            }
        }

        public Task UpdateAsync(Comment comment)
        {

            _commentRepository.Update(comment);
            return _commentRepository.SaveAsync();
        }

        public Task<string?> GetCommentsByBlogIdAsync(int blogId)
        {

            var comments = _commentRepository.GetCommentsByBlogIdAsync(blogId);
            if (comments != null)
            {
                return Task.FromResult(comments.ToString());
            }
            else
            {
                return Task.FromResult<string?>(null);
            }
        }
    }

}
