using DogusBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Categories.Any()) return;

            // Kategoriler
            var categories = new List<Category>
            {
                new() { Name = "Yazılım" },
                new() { Name = "Kitap" },
                new() { Name = "Film" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Tag'ler
            var tags = new List<Tag>
            {
                new() { Name = "ASP.NET" },
                new() { Name = "Entity Framework" },
                new() { Name = "C#" }
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            // Şifre hashleme
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = "testuser",
                Email = "test@example.com"
            };
            user.PasswordHash = hasher.HashPassword(user, "1234"); 

            context.Users.Add(user);
            context.SaveChanges();

            // Blog
            var blog = new Blog
            {
                Title = "İlk Blog",
                Content = "Bu bir test blogudur.",
                PublishDate = DateTime.Now,
                UserId = user.Id,
                CategoryId = categories[0].Id
            };
            context.Blogs.Add(blog);
            context.SaveChanges();

            // BlogTag
            context.BlogTags.AddRange(
                new BlogTag { BlogId = blog.Id, TagId = tags[0].Id },
                new BlogTag { BlogId = blog.Id, TagId = tags[1].Id }
            );
            context.SaveChanges();

            // Yorum
            context.Comments.Add(new Comment
            {
                BlogId = blog.Id,
                UserId = user.Id,
                Content = "Harika bir yazı!",
                CreatedAt = DateTime.Now
            });

            context.SaveChanges();
        }
    }


}
