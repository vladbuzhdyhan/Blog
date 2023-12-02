using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class SiteContext : IdentityDbContext<User>
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options) { }
        public virtual DbSet<News> News { get; set; }
    }
}
