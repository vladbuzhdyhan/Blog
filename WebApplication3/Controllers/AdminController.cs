using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SiteContext _siteContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(SiteContext sitecontext, IWebHostEnvironment webHostEnvironment)
        {
            _siteContext = sitecontext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var newsList = _siteContext.News.ToList();
            return View(newsList);
        }

        public IActionResult Delete(int id)
        {
            var comments = _siteContext.Comments.Where(x => x.NewsId == id);
            var newsItem = _siteContext.News.FirstOrDefault(n => n.Id == id);
            _siteContext.News.Remove(newsItem);
            _siteContext.Comments.RemoveRange(comments);
            _siteContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var newsItem = _siteContext.News.FirstOrDefault(n => n.Id == id);
            if (newsItem == null)
            {
                return NotFound();
            }
            return View(newsItem);
        }

        [HttpPost]
        public IActionResult Create(string title, DateTime date, string text, IFormFile generalImage)
        { 
            if (generalImage != null && generalImage.Length > 0)
            {
                News news = new News();
                news.Title = title;
                news.Date = date;
                news.FullText = text;
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + generalImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    generalImage.CopyTo(fileStream);
                }
                news.ImageUrl = "/images/" + uniqueFileName;
                _siteContext.News.Add(news);
                _siteContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, News editedNews, IFormFile generalImage)
        {
            if (id != editedNews.Id)
            {
                return NotFound();
            }
            var existingNews = _siteContext.News.FirstOrDefault(n => n.Id == id);
            if (existingNews != null)
            {
                if (generalImage != null && generalImage.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + generalImage.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        generalImage.CopyTo(fileStream);
                    }
                    existingNews.ImageUrl = "/images/" + uniqueFileName;
                }
                existingNews.Title = editedNews.Title;
                existingNews.Date = editedNews.Date;
                existingNews.FullText = editedNews.FullText;
                _siteContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedNews);
        }

    }
}
