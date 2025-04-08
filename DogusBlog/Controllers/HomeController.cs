using DogusBlog.Models;
using DogusBlog.Models.Dto;
using DogusBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DogusBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBlogService blogService, ILogger<HomeController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
