//using DogusBlog.Models;
//using DogusBlog.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace DogusBlog.Controllers
//{
//    public class CommentController : Controller
//    {
//        private readonly ICommentService _commentService;
//        private readonly IBlogService _blogService;

//        public CommentController(ICommentService commentService, IBlogService blogService)
//        {
//            _commentService = commentService;
//            _blogService = blogService;
//        }

//        // GET: /Comment/Index?blogId=1
//        public async Task<IActionResult> Index(int blogId)
//        {
//            var comments = await _commentService.GetCommentsByBlogIdAsync(blogId);
//            ViewBag.Blog = await _blogService.GetByIdAsync(blogId);
//            return View(comments);
//        }

//        // GET: /Comment/Create?blogId=1
//        public IActionResult Create(int blogId)
//        {
//            ViewBag.BlogId = blogId;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> AddComment(int BlogId, string Content)
//        {
//            if (string.IsNullOrEmpty(Content))
//            {
//                return RedirectToAction("Details", "Blog", new { id = BlogId });
//            }

//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Auth");
//            }

//            var comment = new Comment
//            {
//                BlogId = BlogId,
//                Content = Content,
//                CreatedAt = DateTime.Now,
//                UserId = int.Parse(userId)
//            };

//            await _commentService.AddAsync(comment);
//            return RedirectToAction("Details", "Blog", new { id = BlogId });
//        }

//        // POST: /Comment/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Comment comment)
//        {
//            if (ModelState.IsValid)
//            {
//                comment.CreatedAt = DateTime.Now;
//                // comment.UserId = CURRENT USER ID 
//                await _commentService.AddAsync(comment);
//                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
//            }

//            ViewBag.BlogId = comment.BlogId;
//            return View(comment);
//        }

//        // GET: /Comment/Edit/5
//        public async Task<IActionResult> Edit(int id)
//        {
//            var comment = await _commentService.GetByIdAsync(id);
//            if (comment == null)
//                return NotFound();

//            return View(comment);
//        }

//        // POST: /Comment/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Comment comment)
//        {
//            if (ModelState.IsValid)
//            {
//                await _commentService.UpdateAsync(comment);
//                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
//            }

//            return View(comment);
//        }

//        // GET: /Comment/Delete/5
//        public async Task<IActionResult> Delete(int id)
//        {
//            var comment = await _commentService.GetByIdAsync(id);
//            if (comment == null)
//                return NotFound();

//            return View(comment);
//        }


//        // POST: /Comment/DeleteConfirmed/5
//        [HttpPost, ActionName("DeleteConfirmed")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var comment = await _commentService.GetByIdAsync(id);
//            if (comment != null)
//            {
//                await _commentService.DeleteAsync(id);
//                return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
//            }

//            return NotFound();
//        }
//    }
//}


using DogusBlog.Models;
using DogusBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogusBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly IUserService _userService; // Kullanıcı bilgilerini almak için ekleyin

        public CommentController(ICommentService commentService, IBlogService blogService, IUserService userService)
        {
            _commentService = commentService;
            _blogService = blogService;
            _userService = userService;
        }

        // GET: /Comment/Index?blogId=1
        public async Task<IActionResult> Index(int blogId)
        {
            var comments = await _commentService.GetCommentsByBlogIdAsync(blogId);
            ViewBag.Blog = await _blogService.GetByIdAsync(blogId);
            return View(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public async Task<IActionResult> AddComment(int BlogId, string Content)
        //{
        //    if (string.IsNullOrEmpty(Content))
        //    {
        //        return RedirectToAction("Details", "Blog", new { id = BlogId });
        //    }

        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    var userIdInt = int.Parse(userId);


        //    var comment = new Comment
        //    {
        //        BlogId = BlogId,
        //        Content = Content,
        //        CreatedAt = DateTime.Now,
        //        UserId = userIdInt
        //    };

        //    await _commentService.AddAsync(comment);
        //    return RedirectToAction("Details", "Blog", new { id = BlogId });
        //}


        public async Task<IActionResult> AddComment(int BlogId, string Content, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(Content))
            {
                return RedirectToAction("Details", "Blog", new { id = BlogId });
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var userIdInt = int.Parse(userId);

            var comment = new Comment
            {
                BlogId = BlogId,
                Content = Content,
                CreatedAt = DateTime.Now,
                UserId = userIdInt
            };
            await _commentService.AddAsync(comment);

            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

           
            return RedirectToAction("Details", "Blog", new { id = BlogId });
        }

        // GET: /Comment/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            // Sadece yorumun sahibi düzenleyebilir
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || comment.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(comment);
        }

        // POST: /Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Comment comment, string returnUrl = null)
        {
            // Sadece yorumun sahibi düzenleyebilir
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || comment.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                // Mevcut yorumu getir
                var existingComment = await _commentService.GetByIdAsync(comment.Id);
                if (existingComment == null)
                    return NotFound();

                // Sadece içeriği güncelle, diğer alanlar aynı kalsın
                existingComment.Content = comment.Content;
                // Değişiklik zamanını güncelleyebiliriz (isteğe bağlı)
                // existingComment.UpdatedAt = DateTime.Now; (Bu alan Comment modelinde varsa)

                await _commentService.UpdateAsync(existingComment);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Details", "Blog", new { id = existingComment.BlogId });
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(comment);
        }
        // POST: /Comment/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            // Sadece yorumun sahibi silebilir
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || comment.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            await _commentService.DeleteAsync(id);
            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }
    }
}