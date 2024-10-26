using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.Domain.Entities;

namespace WEB_253503_MINICH.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
