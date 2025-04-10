using System.ComponentModel.DataAnnotations;

namespace DogusBlog.Models.Dto
{
 
        public class BlogCreateDto
        {
            [Required]
            public string Title { get; set; }

            [Required]
            public string Content { get; set; }

            public IFormFile? Image { get; set; }

            [Required]
            public int CategoryId { get; set; }

            public List<int> SelectedTagIds { get; set; } = new List<int>();

           
            public List<Category> Categories { get; set; } = new();
            public List<Tag> Tags { get; set; } = new();
        }


    }

