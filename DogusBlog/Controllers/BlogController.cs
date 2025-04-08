using DogusBlog.Models;
using DogusBlog.Models.Dto;
using DogusBlog.Models.Dto.DogusBlog.Models.Dto;
using DogusBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogusBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public BlogController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        // GET: /Blog
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();

            var blogDtos = blogs.Select(b => new BlogDto
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                PublishDate = b.PublishDate,
                ImagePath = b.ImagePath,
                CategoryName = b.Category?.Name ?? "Kategorisiz",
                UserName = b.User?.Username ?? "Anonim",
              
                Tags = b.BlogTags?.Select(bt => bt.Tag?.Name).Where(t => t != null).ToList() ?? new List<string>()
            }).ToList();

            return View(blogDtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetBlogWithDetailsAsync(id);
            if (blog == null)
                return NotFound();

            var dto = new BlogDetailDto
            {
                Title = blog.Title,
                Content = blog.Content,
                PublishDate = blog.PublishDate,
                CategoryName = blog.Category?.Name ?? "Kategorisiz",
                UserName = blog.User?.Username ?? "Anonim",
                Comments = blog.Comments?.Select(c => new CommentDto
                {
                    Id = c.Id,  // Id'yi ekledik
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User?.Username ?? "Anonim"
                }).ToList() ?? new List<CommentDto>()
            };

            return View(dto);
        }



        // GET: /Blog/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        // POST: /Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.PublishDate = DateTime.Now;
                // blog.UserId = CURRENT USER ID → sonra ekleyeceğiz
                await _blogService.AddAsync(blog);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(blog);
        }

        // GET: /Blog/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound();

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(blog);
        }

        // POST: /Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                await _blogService.UpdateAsync(blog);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(blog);
        }

        // GET: /Blog/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        // POST: /Blog/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
