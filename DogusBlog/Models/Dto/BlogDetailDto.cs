using DogusBlog.Models.Dto.DogusBlog.Models.Dto;

namespace DogusBlog.Models.Dto
{
    public class BlogDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public List<CommentDto> Comments { get; set; }

        public string? ImagePath { get; set; }
    }

}
