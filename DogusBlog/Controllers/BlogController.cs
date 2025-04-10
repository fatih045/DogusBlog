using DogusBlog.Models;
using DogusBlog.Models.Dto;
using DogusBlog.Models.Dto.DogusBlog.Models.Dto;
using DogusBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogusBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public BlogController(IBlogService blogService, ICategoryService categoryService,ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
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
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                PublishDate = blog.PublishDate,
                CategoryName = blog.Category?.Name ?? "Kategorisiz",
                UserName = blog.User?.Username ?? "Anonim",
                ImagePath = blog.ImagePath,
                Comments = blog.Comments?.Select(c => new CommentDto
                {
                    Id = c.Id,  
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User?.Username ?? "Anonim"
                }).ToList() ?? new List<CommentDto>()
            };

            return View(dto);
        }



        public async Task<IActionResult> Create()
        {
            var dto = new BlogCreateDto
            {
                Categories = (await _categoryService.GetAllAsync()).ToList(),
                Tags = (await _tagService.GetAllAsync()).ToList()
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                dto.Categories = (await _categoryService.GetAllAsync()).ToList();
                dto.Tags = (await _tagService.GetAllAsync()).ToList();
                return View(dto);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var blog = new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                PublishDate = DateTime.Now,
                CategoryId = dto.CategoryId,
                UserId = int.Parse(userIdClaim.Value),
                BlogTags = dto.SelectedTagIds.Select(tagId => new BlogTag
                {
                    TagId = tagId
                }).ToList()
            };

            // Resim yükleme işlemini ekleyelim
            if (dto.Image != null && dto.Image.Length > 0)
            {
                // Uploads klasörü yoksa oluştur
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blogs");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Benzersiz bir dosya adı oluştur
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Dosyayı fiziksel olarak kaydet
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                // Veritabanına kaydedilecek yolu ata
                blog.ImagePath = "/uploads/blogs/" + uniqueFileName;
            }

            await _blogService.AddAsync(blog);

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, message = "Kategori adı boş olamaz." });
            }

            try
            {
               
                var existingCategory = await _categoryService.GetByNameAsync(name);
                if (existingCategory != null)
                {
                    return Json(new { success = false, message = "Bu kategori zaten mevcut." });
                }

                
                var category = new Category { Name = name };
                await _categoryService.AddAsync(category);

                return Json(new { success = true, id = category.Id, name = category.Name });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateTag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, message = "Etiket adı boş olamaz." });
            }

            try
            {
               
                var existingTag = await _tagService.GetByNameAsync(name);
                if (existingTag != null)
                {
                    return Json(new { success = false, message = "Bu etiket zaten mevcut." });
                }

                
                var tag = new Tag { Name = name };
                await _tagService.AddAsync(tag);

                return Json(new { success = true, id = tag.Id, name = tag.Name });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: /Blog/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetBlogWithDetailsAsync(id);
            if (blog == null)
                return NotFound();

           
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || blog.UserId != int.Parse(userIdClaim.Value))
            {
                return Unauthorized();
            }

           
            var editDto = new BlogEditDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CategoryId = blog.CategoryId,
                CurrentImagePath = blog.ImagePath,
                Categories = (await _categoryService.GetAllAsync()).ToList(),
                Tags = (await _tagService.GetAllAsync()).ToList(),
                SelectedTagIds = blog.BlogTags?.Select(bt => bt.TagId).ToList() ?? new List<int>()
            };

            return View(editDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                dto.Categories = (await _categoryService.GetAllAsync()).ToList();
                dto.Tags = (await _tagService.GetAllAsync()).ToList();
                return View(dto);
            }

            var blog = await _blogService.GetByIdAsync(dto.Id);
            if (blog == null)
                return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || blog.UserId != int.Parse(userIdClaim.Value))
            {
                return Unauthorized();
            }

            // Blog verilerini güncelle (Etiketler hariç)
            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.CategoryId = dto.CategoryId;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                // Eski resmi sil (eğer varsa)
                if (!string.IsNullOrEmpty(blog.ImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni resmi kaydet
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blogs");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Benzersiz bir dosya adı oluştur
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Dosyayı fiziksel olarak kaydet
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                // Veritabanına kaydedilecek yolu ata
                blog.ImagePath = "/uploads/blogs/" + uniqueFileName;
            }

            // Önce blog bilgilerini güncelle
            await _blogService.UpdateAsync(blog);

            // Etiketleri güncelle
            await _blogService.UpdateBlogTagsAsync(blog.Id, dto.SelectedTagIds);

            return RedirectToAction("Details", new { id = blog.Id });
        }

        // GET: /Blog/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        // AJAX ile blog silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var blog = await _blogService.GetByIdAsync(id);

                
                if (blog == null)
                    return NotFound();

              
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || blog.UserId != int.Parse(userIdClaim.Value))
                {
                    return Unauthorized();
                }

                
                await _blogService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
