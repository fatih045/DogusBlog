namespace DogusBlog.Models.Dto
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImagePath { get; set; }

        
        public string CategoryName { get; set; }
        public string UserName { get; set; }


       
        public List<string> Tags { get; set; } = new List<string>();
    }
}
