using DogusBlog.Models;
using DogusBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogusBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;

        public CommentController(ICommentService commentService, IBlogService blogService)
        {
            _commentService = commentService;
            _blogService = blogService;
        }

        // GET: /Comment/Index?blogId=1
        public async Task<IActionResult> Index(int blogId)
        {
            var comments = await _commentService.GetCommentsByBlogIdAsync(blogId);
            ViewBag.Blog = await _blogService.GetByIdAsync(blogId);
            return View(comments);
        }

        // GET: /Comment/Create?blogId=1
        public IActionResult Create(int blogId)
        {
            ViewBag.BlogId = blogId;
            return View();
        }

        // POST: /Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.Now;
                // comment.UserId = CURRENT USER ID (login sisteminden sonra eklenecek)
                await _commentService.AddAsync(comment);
                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            }

            ViewBag.BlogId = comment.BlogId;
            return View(comment);
        }

        // GET: /Comment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        // POST: /Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentService.UpdateAsync(comment);
                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            }

            return View(comment);
        }

        // GET: /Comment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        // POST: /Comment/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment != null)
            {
                await _commentService.DeleteAsync(id);
                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            }

            return NotFound();
        }
    }
}
