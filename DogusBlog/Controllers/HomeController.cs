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
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public HomeController(IBlogService blogService,
                       ICategoryService categoryService,
                       ITagService tagService,
                       ILogger<HomeController> logger)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? categoryId, string? tagName)
        {

            var blogs = await _blogService.GetAllAsync();

           
            if (categoryId.HasValue)
            {
                var category = await _categoryService.GetByIdAsync(categoryId.Value);
                blogs = blogs.Where(b => b.Category?.Name == category?.Name).ToList();
            }

         
            if (!string.IsNullOrEmpty(tagName))
            {
                blogs = blogs.Where(b => b.BlogTags.Any(bt => bt.Tag != null && bt.Tag.Name == tagName)).ToList();
            }

          
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

            // Kategori ve Tag listeleri ViewBag'e
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.SelectedTag = tagName;


           

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                var userBlogs = await _blogService.GetBlogsByUserIdAsync(userId);

                var userBlogDtos = userBlogs.Select(b => new BlogDto
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

                ViewBag.UserBlogs = userBlogDtos;
            }




            

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
