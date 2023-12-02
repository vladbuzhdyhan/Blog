using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteContext _siteContext;

        public HomeController(SiteContext sitecontext)
        {
            _siteContext = sitecontext;
        }

        public IActionResult Index()
        {
            var news = _siteContext.News.OrderByDescending(n => n.Date).Take(6).ToList();
            return View(news);
        }
    }
}