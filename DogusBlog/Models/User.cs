using System.Reflection.Metadata;
using System.Xml.Linq;

namespace DogusBlog.Models
{
    public class User
    {


        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string? ProfileImagePath { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }   


    }
}
