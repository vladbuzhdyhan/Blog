using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly SiteContext _siteContext;
        private readonly UserManager<User> _userManager;

        public NewsController(SiteContext sitecontext, UserManager<User> userManager)
        {
            _siteContext = sitecontext;
            _userManager = userManager;
        }

        [Route("{slug}")]
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
                comment.SenderId = _userManager.GetUserId(User);
                comment.SenderName = User.Identity.Name;
                news.Comments.Add(comment);
                _siteContext.SaveChanges();
            }
            return RedirectToAction("Details", new {id = newsId});
        }

        public IActionResult Delete(int id, int newsId)
        {
            var comment = _siteContext.Comments.First(x => x.Id == id);
            _siteContext.Comments.Remove(comment);
            _siteContext.SaveChanges();
            return RedirectToAction("Details", new {id = newsId});
        }
    }
}
