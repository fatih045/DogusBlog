using System.ComponentModel.DataAnnotations;

namespace DogusBlog.Models.Dto
{
    
    public class BlogEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }

        public string? CurrentImagePath { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<int> SelectedTagIds { get; set; } = new List<int>();
    }
}
