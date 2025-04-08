namespace DogusBlog.Models.Dto
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImagePath { get; set; }

        // İlişkili veriler için sadece gerekli bilgileri içerin
        public string CategoryName { get; set; }
        public string UserName { get; set; }


        // Tag'leri string listesi olarak alabiliriz
        public List<string> Tags { get; set; } = new List<string>();
    }
}
