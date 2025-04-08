using DogusBlog.Models;
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
            new() { Id = 1, Name = "Yazılım" },
            new() { Id = 2, Name = "Kitap" },
            new() { Id = 3, Name = "Film" }
        };
            context.Categories.AddRange(categories);

            // Tag'ler
            var tags = new List<Tag>
        {
            new() { Id = 1, Name = "ASP.NET" },
            new() { Id = 2, Name = "Entity Framework" },
            new() { Id = 3, Name = "C#" }
        };
            context.Tags.AddRange(tags);

            // Kullanıcı
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "1234" 
            };
            context.Users.Add(user);

            // Blog
            var blog = new Blog
            {
                Id = 1,
                Title = "İlk Blog",
                Content = "Bu bir test blogudur.",
                PublishDate = DateTime.Now,
                UserId = 1,
                CategoryId = 1
            };
            context.Blogs.Add(blog);

            // BlogTag
            context.BlogTags.AddRange(
                new BlogTag { BlogId = 1, TagId = 1 },
                new BlogTag { BlogId = 1, TagId = 2 }
            );

            // Yorum
            context.Comments.Add(new Comment
            {
                Id = 1,
                BlogId = 1,
                UserId = 1,
                Content = "Harika bir yazı!",
                CreatedAt = DateTime.Now
            });

            context.SaveChanges();
        }
    }

}
