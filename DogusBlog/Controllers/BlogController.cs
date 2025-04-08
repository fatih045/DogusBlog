using DogusBlog.Models;
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
            return View(blogs);
        }

        // GET: /Blog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound();

            return View(blog);
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
