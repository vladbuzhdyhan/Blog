using System.Security.Cryptography.Xml;

namespace WebApplication3.Models
{
    public class News
    {
        public News() { 
            Comments = new HashSet<Comment>();
        }
        public virtual ICollection<Comment> Comments { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string? FullText { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string? Sender { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
