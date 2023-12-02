namespace WebApplication3.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string? FullText { get; set; }
    }
}
