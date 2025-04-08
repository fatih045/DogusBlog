using System.Xml.Linq;

namespace DogusBlog.Models
{
    public class Blog
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public string? ImagePath { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
