using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View(newsItem);
        }
    }
}
