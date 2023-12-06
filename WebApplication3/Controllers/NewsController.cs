using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class NewsController : Controller
    {
        private readonly SiteContext _siteContext;

        public NewsController(SiteContext sitecontext)
        {
            _siteContext = sitecontext;
        }

        public IActionResult Details(int id)
        {
            var newsItem = _siteContext.News.FirstOrDefault(n => n.Id == id);
            var comments = _siteContext.Comments.Where(x => x.NewsId == id).OrderByDescending(x => x.Date).ToList();
            newsItem.Comments = comments;
            return View(newsItem);
        }

        public IActionResult AddComment(int newsId, string text)
        {
            if (!text.IsNullOrEmpty())
            {
                var news = _siteContext.News.FirstOrDefault(n => n.Id == newsId);
                Comment comment = new Comment();
                comment.NewsId = newsId;
                comment.Text = text;
                comment.Date = DateTime.Now;
                comment.Sender = User.Identity?.Name;
                news.Comments.Add(comment);
                _siteContext.SaveChanges();
            }
            return RedirectToAction("Details", new {id = newsId});
        }
    }
}
