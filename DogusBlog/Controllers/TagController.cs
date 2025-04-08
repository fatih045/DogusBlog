using DogusBlog.Models;
using DogusBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogusBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: /Tag
        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllAsync();
            return View(tags);
        }

        // GET: /Tag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.AddAsync(tag);
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: /Tag/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: /Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateAsync(tag);
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: /Tag/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: /Tag/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
