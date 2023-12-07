using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SearchController : Controller
    {
        private readonly SiteContext _siteContext;
        public SearchController(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Find(string title)
        {
            var found = _siteContext.News.Where(x => x.Title == title).OrderByDescending(x => x.Date).ToList();
            return View(found);
        }
    }
}
